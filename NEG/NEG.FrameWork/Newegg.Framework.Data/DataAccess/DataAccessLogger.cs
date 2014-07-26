/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  09/05/2006 08:11:35
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Data.Common;

using Newegg.Framework.Logging;

namespace Newegg.Framework.DataAccess
{
	/// <summary>
	/// Logs critical information for diagnostics and performance improvement.
	/// </summary>
	internal static class DataAccessLogger
	{
		private const string LOG_CATEGORY_NAME = "Framework.DataAccess";

		#region event ids
		private const int LoadDatabaseFile = 1;
		private const int LoadCommandInventoryFile = 2;
		private const int LoadCommandFile = 3;
		private const int DBFileChanged = 10;
		private const int EXECUTION_ERROR = 20;
		#endregion

		[Conditional("TRACE")]
		public static void LogDatabaseFileChanged(FileSystemEventArgs arg)
		{
			//LogEvent(DBFileChanged, arg.FullPath, arg.ChangeType.ToString());
		}

		[Conditional("TRACE")]
		public static void LogDatabaseFileLoaded(string fileName)
		{
			//LogEvent(LoadDatabaseFile, fileName);
		}

		[Conditional("TRACE")]
		public static void LogDatabaseCommandFileLoaded(string fileName)
		{
			//LogEvent(LoadCommandFile, fileName);
		}

		[Conditional("TRACE")]
		public static void LogDataCommandInventoryFileLoaded(string fileName, int count)
		{
			//LogEvent(LoadCommandInventoryFile, fileName + ". " + count.ToString());
		}

		[Conditional("TRACE")]
		public static void LogExecutionError(string conectionString, DbCommand cmd, Exception ex)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\tDataCommand Execution error, command text:");
			sb.Append(cmd.CommandText);
			if (cmd.Parameters != null && cmd.Parameters.Count > 0)
			{
				sb.Append(System.Environment.NewLine);
				sb.Append("Command Parameters:");
				foreach (DbParameter parameter in cmd.Parameters)
				{
					sb.Append(parameter.ParameterName);
					sb.Append("=");
					sb.Append(parameter.Value);
					sb.Append(",");
				}
			}
			sb.Append(System.Environment.NewLine);
			sb.Append("ConnectionString: ").AppendLine(conectionString);
			sb.Append("Exception: ");
			sb.Append(ex.ToString());
			LogEvent(EXECUTION_ERROR, sb.ToString());
		}

		[Conditional("TRACE")]
		public static void LogDatabaseRetry(DbCommand cmd, string connectionString)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\tDatabase retry log, command text:");
			sb.Append(cmd.CommandText);
			sb.Append(System.Environment.NewLine);
			sb.Append("Connection String:");
			sb.Append(connectionString);
			LogEvent(EXECUTION_ERROR, sb.ToString());
		}

		private static void LogEvent(int eventId, params string[] parameters)
		{
			LoggerFactory.CreateLogger().LogEvent(LOG_CATEGORY_NAME, eventId, parameters);
		}
	}
}
