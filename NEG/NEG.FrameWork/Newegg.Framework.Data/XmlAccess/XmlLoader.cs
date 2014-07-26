/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/06/2006 16:44:57
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;

using Newegg.Framework.Entity;
using Newegg.Framework.Logging;
using Newegg.Framework.XmlAccess.Configuration;
using System.Web;
using System.IO;

namespace Newegg.Framework.XmlAccess
{
	internal static class XmlLoader
	{
		/// <summary>
		/// Loads the data table.
		/// Returns null if failed to load the xml file.
		/// </summary>
		/// <param name="xmlFileName">Name of the XML file.</param>
		/// <returns></returns>
		private static DataTable LoadXmlDataTable(string path, string xmlFileName, ref Exception exception)
		{
			string xmlFileFullPath = GetFileFullPath(path, xmlFileName);
			
			// if file does not exist, no log
			if (!File.Exists(xmlFileFullPath))
			{
				return null;
			}
			try
			{
				DataSet ds = new DataSet();
				XmlReadMode mode = ds.ReadXml(xmlFileFullPath, XmlReadMode.ReadSchema);
				return ds.Tables[0];
			}
			catch (Exception ex)
			{
				LogFailToLoadXml(xmlFileFullPath, ex);
				exception = ex;
				return null;
			}
		}

		/// <summary>
		/// Loads the data table.
		/// </summary>
		/// <param name="xmlFileName">Name of the XML file.</param>
		/// <returns></returns>
		public static DataTable LoadDataTable(string xmlFileName)
		{
			DataTable dt;
			Exception lastException = null;

			// try default folder
			dt = LoadXmlDataTable(ConfigurationManager.XmlAccessConfiguration.DefaultXmlDataFolder, xmlFileName, ref lastException);
			if (dt != null)
			{
				return dt;
			}

			// retry for alternative folders
			if (ConfigurationManager.XmlAccessConfiguration.AlternateXmlDataFolders == null ||
				ConfigurationManager.XmlAccessConfiguration.AlternateXmlDataFolders.Folders == null ||
				ConfigurationManager.XmlAccessConfiguration.AlternateXmlDataFolders.Folders.Count == 0)
			{
				return null;
			}

			string path = null;
			for (int i = 0; i < ConfigurationManager.XmlAccessConfiguration.AlternateXmlDataFolders.Folders.Count; i ++)
			{
				path = ConfigurationManager.XmlAccessConfiguration.AlternateXmlDataFolders.Folders[i];
				dt = LoadXmlDataTable(path, xmlFileName, ref lastException);
				if (dt != null)
				{
					return dt;
				}
			}

			if (lastException != null)
			{
				LogRetryLoadXml(path, xmlFileName, lastException);
			}
			return null;
		}

		#region private helper function
		private static string GetFileFullPath(string virtualPath, string relativePath)
		{
			string path = string.Empty;
			if (HttpContext.Current != null)
			{
				path = HttpContext.Current.Server.MapPath(virtualPath);
			}
			else
			{
				path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, virtualPath.Replace('/', '\\').TrimStart('~').TrimStart('\\'));
			}
			return Path.Combine(path, relativePath);
		}
		#endregion

		#region logger
		private const string LOG_CATEGORY = "Framework.XmlAccess";
		private const int EVENT_FAIL_TO_RETRY = 1;
		private const int EVENT_FAIL_TO_LOAD_XML = 2;

		[Conditional("TRACE")]
		private static void LogFailToLoadXml(string fileName, Exception ex)
		{
			string message = "Fail to load xml file: " + fileName + System.Environment.NewLine;
			message += "Exception: " + ex.ToString();
			LoggerFactory.CreateLogger().LogEvent(LOG_CATEGORY, EVENT_FAIL_TO_LOAD_XML, message);
		}

		[Conditional("TRACE")]
		private static void LogRetryLoadXml(string path, string xmlFileName, Exception ex)
		{
			string xmlFileFullPath = GetFileFullPath(path, xmlFileName);
			string message = "XML retry failed: xml file name:" + xmlFileName + ". Last full path: [" + xmlFileFullPath + "]" + 
				System.Environment.NewLine + "\t Exception:" + 
				System.Environment.NewLine + ex.ToString();
			LoggerFactory.CreateLogger().LogEvent(LOG_CATEGORY, EVENT_FAIL_TO_RETRY, message);
		}
		#endregion
	}
}