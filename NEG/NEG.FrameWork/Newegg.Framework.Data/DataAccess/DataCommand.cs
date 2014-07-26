/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/26/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 2007/08/03	JasonHuang			如果Property DatabaseName为空不会抛出异常，应对配置文件中没有制定DatabaseName的场景
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Collections;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Newegg.Framework.DataAccess.Configuration;
using Newegg.Framework.Entity;
using Newegg.Framework.Utility;
using Newegg.Framework.Collection;

namespace Newegg.Framework.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class DataCommand : ICloneable, IDisposable
    {
        protected DbCommand m_DbCommand;
        protected string m_DatabaseName;
        private bool m_SupportTransaction;
        private DbTransaction m_Transaction;
        private DbConnection m_Connection;
        private IDictionary<string, DataOperationParameterGroup> m_ParameterGroupDictionary = new Dictionary<string, DataOperationParameterGroup>(new CaseInsensitiveStringEqualityComparer());
        private HisQueryManager m_HisQueryManager = new HisQueryManager();

        internal DbCommand DbCommand
        {
            get { return m_DbCommand; }
        }
        internal IDictionary<string, DataOperationParameterGroup> ParameterGroupDictionary
        {
            set { m_ParameterGroupDictionary = value; }
            get { return m_ParameterGroupDictionary; }
        }

        //lynn 2007-4-21
        private DatabaseInstance m_DbInstance;
        private int m_ConnectionStringIndex = 0;
        private int m_CurrentRetryCount = 0;

        #region constructors

        internal DataCommand(string databaseName, DbCommand command)
        {
            DatabaseName = databaseName;
            m_DbCommand = command;
            m_SupportTransaction = false;
        }
        private DataCommand()
        {
        }

        public DataCommand HisQueryMaintainDataCommand
        {
            get
            {
                return m_HisQueryManager.HisQueryMaintainDataCommand;
            }
            set
            {
                m_HisQueryManager.HisQueryMaintainDataCommand = value;
            }
        }

        public object Clone()
        {
            DataCommand cmd = new DataCommand();
            cmd.m_DbCommand = CloneCommand(this.m_DbCommand);
            cmd.DatabaseName = m_DatabaseName;
            cmd.ParameterGroupDictionary = ParameterGroupDictionary;

            return cmd;
        }

        /// <summary>
        /// Clones the command.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <returns></returns>
        private static DbCommand CloneCommand(DbCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            if (cmd is ICloneable)
            {
                return ((ICloneable)cmd).Clone() as DbCommand;
            }
            else
            {
                throw new ApplicationException("A class that implements IClonable is expected.");
            }
        }

        /// <summary>
        /// Copies the command from command while keeping the transaction context.
        /// </summary>
        /// <param name="command">The command.</param>
        internal void CopyCommand(DataCommand command)
        {
            m_DbCommand = CloneCommand(command.m_DbCommand);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this command is executed in a support transaction.
        /// If the command is executed in a transaction and the transaction is not spcifically committed using CommitTransaction(),
        /// the underlying transaction will be rolled back.
        /// </summary>
        /// <value><c>true</c> if [support transaction]; otherwise, <c>false</c>.</value>
        public bool SupportTransaction
        {
            get
            {
                return m_SupportTransaction;
            }
            internal set
            {
                m_SupportTransaction = value;
            }
        }

        protected virtual Database ActualDatabase
        {
            // Note: use late binding to reflect the real configuration.
            get 
            {
                if (m_HisQueryManager.IsHisQuery)
                {
                    return new SqlDatabase(m_HisQueryManager.CurrentConnectionString);
                }
                else
                {
                    return new SqlDatabase(m_DbInstance.ConnectionStringList[m_ConnectionStringIndex]);
                }
            }
        }

        public string DatabaseName
        {
            set
            {
                m_DatabaseName = value;
                if (!string.IsNullOrEmpty(value))
                {
                    m_DbInstance = DatabaseManager.GetDatabaseInstance(value);
                }
            }
            get
            {
                return m_DatabaseName;
            }
        }
        #endregion

        #region parameters
        /// <summary>
        /// get a parameter value
        /// </summary>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public object GetParameterValue(string paramName)
        {
            return ActualDatabase.GetParameterValue(m_DbCommand, paramName);
        }

        /// <summary>
        /// set a parameter value 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="val"></param>
        public void SetParameterValue(string paramName, object val)
        {
            ActualDatabase.SetParameterValue(m_DbCommand, paramName, val);
        }
        #endregion

        #region transaction operation
        private void PrepareTransaction()
        {
            if (m_SupportTransaction && m_Transaction == null)
            {
                m_Connection = ActualDatabase.CreateConnection();
                try
                {
                    m_Connection.Open();
                }
                catch
                {
                    m_Connection = null;
                    return;
                }
                try
                {
                    m_Transaction = m_Connection.BeginTransaction();
                }
                catch
                {
                    m_Connection.Close();
                    m_Connection = null;
                    m_Transaction = null;
                }
            }
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                m_Transaction.Commit();
            }
            finally
            {
                if (m_SupportTransaction && m_Connection != null)
                {
                    m_Connection.Close();
                }
                m_Connection = null;
                m_Transaction = null;
            }
        }

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                m_Transaction.Rollback();
            }
            finally
            {
                if (m_SupportTransaction && m_Connection != null)
                {
                    m_Connection.Close();
                }
                m_Connection = null;
                m_Transaction = null;
            }
        }

        #endregion

        #region db retry
        /// <summary>
        /// Moves to next db.
        /// returns false if no more db exists.
        /// </summary>
        /// <returns></returns>
        protected virtual bool ProcessException(Exception ex)
        {
            if (m_HisQueryManager.IsHisQuery)
            {
                return m_HisQueryManager.ProcessHisQueryException(ex);
            }

            //if ex == null, initialize retry count
            if (ex == null)
            {
                m_CurrentRetryCount = 0;
            }
            else
            {
                //logging retried command & connection string
                //DataAccessLogger.LogDatabaseRetry(m_DbCommand, m_DbInstance.ConnectionStringList[m_ConnectionStringIndex]);

                //retry count >= connection count, then return false (no retry);
                m_CurrentRetryCount++;
                if (m_CurrentRetryCount >= m_DbInstance.ConnectionStringList.Count)
                {
                    return false;
                }

                //retry all exception
                //if not connection time out, then return false (no retry);
                //if (ex.Message.IndexOf("Timeout expired.") == -1)
                //{
                //    return false;
                //}

                //move next connection Index
                m_ConnectionStringIndex++;

                //if index >= connection count, then move to lower bound index
                if (m_ConnectionStringIndex >= m_DbInstance.ConnectionStringList.Count)
                {
                    m_ConnectionStringIndex = 0;
                }
            }

            return true;
        }
        #endregion

        #region execution
        /// <summary>
        /// Executes the scalar.
        /// Throws an exception if an error occurs.
        /// </summary>
        /// <returns></returns>
        public T ExecuteScalar<T>()
        {
            Exception ex = null;
            while (ProcessException(ex))
            {
                try
                {
                    return DoExecuteScalar<T>();
                }
                catch (Exception iex)
                {
                    ex = iex;
                }
            }
			if (ex != null)
			{
				DataAccessLogger.LogExecutionError(ActualDatabase.ConnectionStringWithoutCredentials, m_DbCommand, ex);
				throw ex;
			}
			throw new Exception("ALL HISQUERY MAINTENANCE");
        }

        /// <summary>
        /// Does the execute scalar.
        /// two functions:
        /// log any exception.
        /// rethrow
        /// </summary>
        /// <returns></returns>
        private T DoExecuteScalar<T>()
        {
            try
            {
                PrepareTransaction();
                if (m_Transaction == null)
                {
                    return (T)ActualDatabase.ExecuteScalar(m_DbCommand);
                }
                else
                {
                    return (T)ActualDatabase.ExecuteScalar(m_DbCommand, m_Transaction);
                }
            }
            catch (Exception ex)
            {
                //DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw;
            }
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <returns></returns>
        public object ExecuteScalar()
        {
            Exception ex = null;
            while (ProcessException(ex))
            {
                try
                {
                    return DoExecuteScalar();
                }
                catch (Exception iex)
                {
                    ex = iex;
                }
            }
			if (ex != null)
			{
				DataAccessLogger.LogExecutionError(ActualDatabase.ConnectionStringWithoutCredentials, m_DbCommand, ex);
				throw ex;
			}
			throw new Exception("ALL HISQUERY MAINTENANCE");
        }
        private object DoExecuteScalar()
        {
            try
            {
                PrepareTransaction();
                if (m_Transaction == null)
                {
                    return ActualDatabase.ExecuteScalar(m_DbCommand);
                }
                else
                {
                    return ActualDatabase.ExecuteScalar(m_DbCommand, m_Transaction);
                }
            }
            catch (Exception ex)
            {
                //DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw;
            }
        }

        /// <summary>
        /// returns the number of rows affected.
        /// </summary>
        /// <returns></returns>
        public int ExecuteNonQuery()
        {
            Exception ex = null;
            while (ProcessException(ex))
            {
                try
                {
                    return DoExecuteNonQuery();
                }
                catch (Exception iex)
                {
                    ex = iex;
                }
            }

			if (ex != null)
			{
				DataAccessLogger.LogExecutionError(ActualDatabase.ConnectionStringWithoutCredentials, m_DbCommand, ex);
				throw ex;
			}
			throw new Exception("ALL HISQUERY MAINTENANCE");
        }
        private int DoExecuteNonQuery()
        {
            try
            {
                PrepareTransaction();
                if (m_Transaction == null)
                {
                    return ActualDatabase.ExecuteNonQuery(m_DbCommand);
                }
                else
                {
                    return ActualDatabase.ExecuteNonQuery(m_DbCommand, m_Transaction);
                }
            }
            catch (Exception ex)
            {
                //DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw;
            }
        }

        /// <summary>
        /// Executes the entity.
        /// Returns null if no entity is returned or the execution failed.
        /// </summary>
        /// <returns></returns>
        public T ExecuteEntity<T>() where T : class, new()
        {
            Exception ex = null;
            while (ProcessException(ex))
            {
                try
                {
                    return DoExecuteEntity<T>();
                }
                catch (Exception iex)
                {
                    ex = iex;
                }
            }

			if (ex != null)
			{
				DataAccessLogger.LogExecutionError(ActualDatabase.ConnectionStringWithoutCredentials, m_DbCommand, ex);
				throw ex;
			}
			throw new Exception("ALL HISQUERY MAINTENANCE");
        }
        private T DoExecuteEntity<T>() where T : class, new()
        {
            IDataReader reader = null;
            try
            {
                reader = ActualDatabase.ExecuteReader(m_DbCommand);
                if (reader.Read())
                {
                    return EntityBuilder.BuildEntity<T>(reader);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// Executes the entity list.
        /// Returns an empty list if no entity is returned or the execution fails.
        /// </summary>
        /// <returns></returns>
        public List<T> ExecuteEntityList<T>() where T : class, new()
        {
            Exception ex = null;
            while (ProcessException(ex))
            {
                try
                {
                    return DoExecuteEntityList<T>();
                }
                catch (Exception iex)
                {
                    ex = iex;
                }
            }
			if (ex != null)
			{
				DataAccessLogger.LogExecutionError(ActualDatabase.ConnectionStringWithoutCredentials, m_DbCommand, ex);
				throw ex;
			}
			throw new Exception("ALL HISQUERY MAINTENANCE");
			
        }
        private List<T> DoExecuteEntityList<T>() where T : class, new()
        {
            IDataReader reader = null;
            try
            {
                reader = ActualDatabase.ExecuteReader(m_DbCommand);
                List<T> list = new List<T>();
                while (reader.Read())
                {
                    T entity = EntityBuilder.BuildEntity<T>(reader);
                    list.Add(entity);
                }
                return list;
            }
            catch (Exception ex)
            {
                //DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>Use with caution. Remember to dispose the returned reader.</remarks>
        public IDataReader ExecuteDataReader()
        {
            Exception ex = null;
            while (ProcessException(ex))
            {
                try
                {
                    return DoExecuteDataReader();
                }
                catch (Exception iex)
                {
                    ex = iex;
                }
            }

			if (ex != null)
			{
				DataAccessLogger.LogExecutionError(ActualDatabase.ConnectionStringWithoutCredentials, m_DbCommand, ex);
				throw ex;
			}
			throw new Exception("ALL HISQUERY MAINTENANCE");
        }
        private IDataReader DoExecuteDataReader()
        {
            try
            {
                return ActualDatabase.ExecuteReader(m_DbCommand);
            }
            catch (Exception ex)
            {
                //DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw;
            }
        }

        /// <summary>
        /// Executes the data set.
        /// </summary>
        /// <returns></returns>
        public DataSet ExecuteDataSet()
        {
            Exception ex = null;
            while (ProcessException(ex))
            {
                try
                {
                    return DoExecuteDataSet();
                }
                catch (Exception iex)
                {
                    ex = iex;
                }
            }

			if (ex != null)
			{
				DataAccessLogger.LogExecutionError(ActualDatabase.ConnectionStringWithoutCredentials, m_DbCommand, ex);
				throw ex;
			}
			throw new Exception("ALL HISQUERY MAINTENANCE");
        }
        private DataSet DoExecuteDataSet()
        {
            try
            {
                return ActualDatabase.ExecuteDataSet(m_DbCommand);
            }
            catch (Exception ex)
            {
                //DataAccessLogger.LogExecutionError(m_DbCommand, ex);
                throw;
            }
        }

        /// <summary>
        /// Executes the data table.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">when db execute error,throw a exception</exception>
        public DataTable ExecuteDataTable()
        {
            return ExecuteDataSet().Tables[0];
        }
        #endregion

        #region IDispose
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            // if called in Dispose, commit transaction.
            // otherwise, let the runtime perform GC.
            if (isDisposing)
            {
                CommitTransaction();
            }
        }
        #endregion

        #region replace token
        public void FormatToken(params string[] tokens)
        {
            m_DbCommand.CommandText = string.Format(m_DbCommand.CommandText, tokens);
        }

        public void Replace(string replaceStatement, string replaceString)
        {
            Regex regex = new Regex(replaceStatement, RegexOptions.IgnoreCase);

            m_DbCommand.CommandText = regex.Replace(m_DbCommand.CommandText, replaceString);
        }

        #endregion

        #region SetParameterGroupValue
        public void SetParameterGroupValue(string name, string parameters)
        {
            AssertUtils.ArgumentHasText(parameters, "parameters");
            SetParameterGroupValue(name, parameters.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void SetParameterGroupValue(string name, ICollection parameters)
        {
            AssertUtils.ArgumentHasText(name, "name");
            if (!ParameterGroupDictionary.ContainsKey(name))
            {
                throw new ArgumentException("current command doesn't contain the parameter.", "name");
            }
            DataOperationParameterGroup paramGroup = ParameterGroupDictionary[name];
            AddInputParametersWithSize(paramGroup.Name, paramGroup.DbType, paramGroup.Size, parameters);
        }

        protected void AddInputParametersWithSize(string name, DbType type, int size, ICollection parameters)
        {
            AssertUtils.ArgumentHasText(name, "name");
            AssertUtils.ArgumentHasLength(parameters, "parameters");
            string parameterNames = string.Empty;
            string currentParameterName;
            int i = 0;
            foreach (object parameter in parameters)
            {
                currentParameterName = name + i;
                parameterNames += currentParameterName + ",";
                if (size <= 0)
                {
                    ActualDatabase.AddInParameter(m_DbCommand, currentParameterName, type, parameter);
                }
                else
                {
                    AddInputParameterWithSize(currentParameterName, type, size, parameter);
                }
                i++;
            }
            string oldParameter = string.Format("({0})", name);
            string newParameter = string.Format("({0})", parameterNames.Substring(0, parameterNames.Length - 1));
            m_DbCommand.CommandText = m_DbCommand.CommandText.Replace(oldParameter, newParameter);
        }
        #endregion

        #region addinputparameters
        /// <summary>
        /// Adds the input parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataType">Type of the db.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        protected void AddInputParameterWithSize(string name, DbType dataType, int size, object value)
        {
            ActualDatabase.AddParameter(m_DbCommand, name, dataType, size, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, value);
        }
        #endregion
    }
}
