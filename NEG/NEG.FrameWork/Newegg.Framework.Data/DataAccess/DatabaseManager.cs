/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/26/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Permissions;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Newegg.Framework.Utility;

namespace Newegg.Framework.DataAccess
{
	internal static class DatabaseManager
	{
		private static Dictionary<string, DatabaseInstance> s_DatabaseInstanceHashtable;
        private static Dictionary<string, DatabaseGroup> s_DatabaseGroupHashtable;
		private static FileSystemWatcher s_Watcher;
		private static FileSystemChangeEventHandler s_FileChangeHandler;

		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		static DatabaseManager()
		{
			s_DatabaseInstanceHashtable = new Dictionary<string, DatabaseInstance>();
			s_FileChangeHandler = new FileSystemChangeEventHandler(500);
			s_FileChangeHandler.ActualHandler += new FileSystemEventHandler(OnFileChanged);
			
			// set up file system watcher
			string databaseFolder = Path.GetDirectoryName(DataAccessSetting.DatabaseConfigFile);
			string databaseFile = Path.GetFileName(DataAccessSetting.DatabaseConfigFile);
			s_Watcher = new FileSystemWatcher(databaseFolder);
			s_Watcher.Filter = databaseFile;
			s_Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
			s_Watcher.Changed += new FileSystemEventHandler(s_FileChangeHandler.ChangeEventHandler);
			s_Watcher.EnableRaisingEvents = true;

			// load database
			LoadDatabaseInstanceList();
		}

		private static void OnFileChanged(object sender, FileSystemEventArgs e)
		{
			//DataAccessLogger.LogDatabaseFileChanged(e);
			LoadDatabaseInstanceList();
		}

		//lynn 2007-4-21
		private static void LoadDatabaseInstanceList()
		{
			DatabaseList list = GetDatabaseList();
			if (list == null || list.DatabaseGroups == null || list.DatabaseGroups.Length == 0)
			{
				throw new DatabaseNotSpecifiedException();
			}
			// convert DatabaseList to a hashtable
			Dictionary<string, DatabaseInstance> databaseInstances = new Dictionary<string, DatabaseInstance>(StringComparer.InvariantCultureIgnoreCase);
			Dictionary<string, DatabaseGroup> databaseGroups = new Dictionary<string, DatabaseGroup>(StringComparer.InvariantCultureIgnoreCase);
			foreach (DatabaseGroup group in list.DatabaseGroups)
			{
				foreach (DatabaseInstance instance in group.DatabaseInstances)
				{
					databaseInstances.Add(instance.Name, instance);
					databaseGroups.Add(instance.Name, group);
				}
			}

			s_DatabaseInstanceHashtable = databaseInstances;
			s_DatabaseGroupHashtable = databaseGroups;
		}

		private static DatabaseList GetDatabaseList()
		{
			return ObjectXmlSerializer.LoadFromXml<DatabaseList>(DataAccessSetting.DatabaseConfigFile);
		}        

		/// <summary>
		/// Gets the database instance.
		/// lynn 2007-4-21
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
        public static DatabaseInstance GetDatabaseInstance(string instanceName)
		{
            return s_DatabaseInstanceHashtable[instanceName];
		}

        /// <summary>
        /// 根据连接字符串获取所属的DatabaseGroup
        /// </summary>
        /// <param name="instanceName">连接字符串</param>
        /// <returns></returns>
        public static DatabaseGroup GetDatabaseGroup(string instanceName)
        {
            return s_DatabaseGroupHashtable[instanceName];
        }
	}
}