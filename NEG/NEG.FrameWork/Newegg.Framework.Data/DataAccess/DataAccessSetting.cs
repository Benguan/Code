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
using System.Configuration;
using System.IO;

using Newegg.Framework.Utility;

namespace Newegg.Framework.DataAccess
{
	/// <summary>
	/// provide basic runtime environment settings for data access component.
	/// </summary>
	internal static class DataAccessSetting
	{
		private static string s_DatabaseConfigFile;
		private static string s_DataCommandFileListConfigFile;

		static DataAccessSetting()
		{
			s_DatabaseConfigFile = ConfigurationHelper.GetConfigurationFile("DatabaseListFile");
			s_DataCommandFileListConfigFile = ConfigurationHelper.GetConfigurationFile("DataCommandFile");
		}

		/// <summary>
		/// get the configuration file for database settings
		/// </summary>
		public static string DatabaseConfigFile
		{
			get { return s_DatabaseConfigFile; }
		}

		/// <summary>
		/// get the configuration file that contains the list of files for datacommand
		/// </summary>
		public static string DataCommandFileListConfigFile
		{
			get { return s_DataCommandFileListConfigFile; }
		}

	}
}
