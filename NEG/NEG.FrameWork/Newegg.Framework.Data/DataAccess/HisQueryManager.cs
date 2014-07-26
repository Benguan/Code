/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Allen Wang (Allen.G.Wang@newegg.com)
 * Create Date:  07/07/2010
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Newegg.Framework.DataAccess
{
    /// <summary>
    /// 历史查询管理。
    /// </summary>
    internal class HisQueryManager
    {
        private static Dictionary<string, List<string>> s_DatabaseConnectionStringList
            = new Dictionary<string, List<string>>(StringComparer.InvariantCultureIgnoreCase);
        private static object s_SyncObj = new object();
        private int m_ConnectionStringIndex = 0;
        
        private DataCommand m_HisQueryMaintainDataCommand;

        public HisQueryManager()
        {
        }

        /// <summary>
        /// 历史数据库的查询命令。
        /// </summary>
        public DataCommand HisQueryMaintainDataCommand
        {
            get
            {
                return m_HisQueryMaintainDataCommand;
            }
            set
            {
                m_HisQueryMaintainDataCommand = value;
            }
        }

        /// <summary>
        /// 是对历史数据库的查询。
        /// </summary>
        public bool IsHisQuery
        {
            get { return m_HisQueryMaintainDataCommand != null; }
        }

        /// <summary>
        /// 当前的连接字符串。
        /// </summary>
        public string CurrentConnectionString
        {
            get
            {
                string connectionString = string.Empty;
                List<string> connectionStringList = GetConnectionStringList();
                if (connectionStringList != null && connectionStringList.Count > 0)
                {
                    connectionString = connectionStringList[m_ConnectionStringIndex];
                }
                return connectionString;
            }
        }

        /// <summary>
        /// 执行历史查询DBRetry检测。
        /// </summary>
        /// <returns>
        /// 1) 如果没有找到匹配的数据库连接则返回<b>false</b>。
        /// 2) 否则，对数据库连接字符串循环检测，一旦碰到没有Maintain的数据库则返回<b>true</b>， 否则返回<b>false</b>。
        /// </returns>
        /// <remarks>
        /// 为DB Retry作判断，如果返回为<b>true</b>则用当前的连接字符串执行HisQuery
        /// </remarks>
        public bool ProcessHisQueryException(Exception ex)
        {
            bool needRetry = false;
            List<string> connectionStringList = GetConnectionStringList();            
            if (connectionStringList==null || connectionStringList.Count==0)
            { // 如果没有找到匹配的数据库连接，则不需要重试
                needRetry = false;
            }
            else
            {
                if (ex == null)
                { // 初始化
                    m_ConnectionStringIndex = -1;
                }

                m_ConnectionStringIndex++;
                for (int i = m_ConnectionStringIndex; i < connectionStringList.Count; i++)
                {
                    m_ConnectionStringIndex = i;
                    if (!IsHisQueryMaintain(connectionStringList[i]))
                    { // 如果遇到没有在维护的数据库则在当前数据库执行HisQuery
                        needRetry = true;
                        break;
                    }
                }

                if (m_ConnectionStringIndex >= connectionStringList.Count)
                {
                    m_ConnectionStringIndex = 0;
                    needRetry = false;
                }
            }

            return needRetry;
        }

        /// <summary>
        /// 判断连接字符串对应的数据库是否在维护。
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>如果在维护则返回<b>true</b>, 反之返回<b>false</b>。</returns>
        public bool IsHisQueryMaintain(string connectionString)
        {
            bool result = false;

            if (m_HisQueryMaintainDataCommand != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string commandText = m_HisQueryMaintainDataCommand.DbCommand.CommandText;
                        DbCommand command = m_HisQueryMaintainDataCommand.DbCommand;
                        command.Connection = connection;
                        connection.Open();
                        using (IDataReader dr = command.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                try
                                {
                                    result = Convert.ToBoolean(Convert.ToInt32(dr[0]));
                                }
                                catch
                                {
                                    result = false;
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    DataAccessLogger.LogExecutionError(connectionString, m_HisQueryMaintainDataCommand.DbCommand, ex);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取当前HisQueryMaintain数据库命令所对应的数据库连接字符串列表。
        /// 字符串连接集中的顺序：
        /// 1） 先获取当前HisQueryMaintain命令所对应的连接字符串列表。
        /// 2） 再获取当前HisQueryMaintain命令所对应的DatabaseGroup中排除了1)中的所有连接字符串列表。
        /// </summary>
        /// <returns>数据库连接字符串列表。</returns>
        /// <remarks>
        /// 先Retry当前的数据库连接字符串，再顺序Retry当前数据库组中的连接字符串。
        /// </remarks>
        public List<string> GetConnectionStringList()
        {
            List<string> connectionStringList = null;
            if (m_HisQueryMaintainDataCommand != null)
            {
                string databaseName = m_HisQueryMaintainDataCommand.DatabaseName;
                if (!s_DatabaseConnectionStringList.TryGetValue(databaseName, out connectionStringList))
                {
                    lock (s_SyncObj)
                    {
                        if (!s_DatabaseConnectionStringList.TryGetValue(databaseName, out connectionStringList))
                        {
							connectionStringList = new List<string>();
                            // 获取当前数据库连接所对应的连接字符串列表
                            DatabaseInstance currentDatabaseInstance = DatabaseManager.GetDatabaseInstance(databaseName);
                            if (currentDatabaseInstance != null)
                            {
                                foreach (string connectionString in currentDatabaseInstance.ConnectionStringList)
                                {
                                    connectionStringList.Add(connectionString);
                                }
                            }

                            // 获取当前数据库连接所对应的DatabaseGroup中所对应的连接字符串列表
                            // 其中不包含当前数据库连接所对应的连接字符串列表
                            DatabaseGroup databaseGroup = DatabaseManager.GetDatabaseGroup(databaseName);
                            if (databaseGroup != null)
                            {
                                DatabaseInstance[] databaseInstances = databaseGroup.DatabaseInstances;
                                if (databaseInstances != null && databaseInstances.Length > 0)
                                {
                                    foreach (DatabaseInstance databaseInstance in databaseInstances)
                                    {
                                        if (databaseInstance != null
                                            && !databaseInstance.Name.Equals(databaseName, StringComparison.InvariantCultureIgnoreCase)
                                            && databaseInstance.ConnectionStringList != null
                                            && databaseInstance.ConnectionStringList.Count > 0)
                                        {
                                            foreach (string connectionString in databaseInstance.ConnectionStringList)
                                            {
                                                connectionStringList.Add(connectionString);
                                            }
                                        }
                                    }
                                }
                            }

                            if (connectionStringList != null && connectionStringList.Count > 0)
                            {
                                s_DatabaseConnectionStringList.Add(databaseName, connectionStringList);
                            }
                        }
                    }
                }
            }

            return connectionStringList;
        }
    }
}