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
    /// ��ʷ��ѯ����
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
        /// ��ʷ���ݿ�Ĳ�ѯ���
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
        /// �Ƕ���ʷ���ݿ�Ĳ�ѯ��
        /// </summary>
        public bool IsHisQuery
        {
            get { return m_HisQueryMaintainDataCommand != null; }
        }

        /// <summary>
        /// ��ǰ�������ַ�����
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
        /// ִ����ʷ��ѯDBRetry��⡣
        /// </summary>
        /// <returns>
        /// 1) ���û���ҵ�ƥ������ݿ������򷵻�<b>false</b>��
        /// 2) ���򣬶����ݿ������ַ���ѭ����⣬һ������û��Maintain�����ݿ��򷵻�<b>true</b>�� ���򷵻�<b>false</b>��
        /// </returns>
        /// <remarks>
        /// ΪDB Retry���жϣ��������Ϊ<b>true</b>���õ�ǰ�������ַ���ִ��HisQuery
        /// </remarks>
        public bool ProcessHisQueryException(Exception ex)
        {
            bool needRetry = false;
            List<string> connectionStringList = GetConnectionStringList();            
            if (connectionStringList==null || connectionStringList.Count==0)
            { // ���û���ҵ�ƥ������ݿ����ӣ�����Ҫ����
                needRetry = false;
            }
            else
            {
                if (ex == null)
                { // ��ʼ��
                    m_ConnectionStringIndex = -1;
                }

                m_ConnectionStringIndex++;
                for (int i = m_ConnectionStringIndex; i < connectionStringList.Count; i++)
                {
                    m_ConnectionStringIndex = i;
                    if (!IsHisQueryMaintain(connectionStringList[i]))
                    { // �������û����ά�������ݿ����ڵ�ǰ���ݿ�ִ��HisQuery
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
        /// �ж������ַ�����Ӧ�����ݿ��Ƿ���ά����
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>�����ά���򷵻�<b>true</b>, ��֮����<b>false</b>��</returns>
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
        /// ��ȡ��ǰHisQueryMaintain���ݿ���������Ӧ�����ݿ������ַ����б�
        /// �ַ������Ӽ��е�˳��
        /// 1�� �Ȼ�ȡ��ǰHisQueryMaintain��������Ӧ�������ַ����б�
        /// 2�� �ٻ�ȡ��ǰHisQueryMaintain��������Ӧ��DatabaseGroup���ų���1)�е����������ַ����б�
        /// </summary>
        /// <returns>���ݿ������ַ����б�</returns>
        /// <remarks>
        /// ��Retry��ǰ�����ݿ������ַ�������˳��Retry��ǰ���ݿ����е������ַ�����
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
                            // ��ȡ��ǰ���ݿ���������Ӧ�������ַ����б�
                            DatabaseInstance currentDatabaseInstance = DatabaseManager.GetDatabaseInstance(databaseName);
                            if (currentDatabaseInstance != null)
                            {
                                foreach (string connectionString in currentDatabaseInstance.ConnectionStringList)
                                {
                                    connectionStringList.Add(connectionString);
                                }
                            }

                            // ��ȡ��ǰ���ݿ���������Ӧ��DatabaseGroup������Ӧ�������ַ����б�
                            // ���в�������ǰ���ݿ���������Ӧ�������ַ����б�
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