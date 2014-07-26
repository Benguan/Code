/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  11/10/2006 15:10:44
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Diagnostics;

using Newegg.Framework.Logging;

namespace Newegg.Framework.XmlAccess
{
	internal static class XmlDataManagerLogger
	{
		private const string LOG_CATEGORY_STRING = "Framework.XmlDataManager";

		#region event ids
		private const int EVENT_CACHE_HIT = 1;
		private const int EVENT_DATA_FILE_RELOADED = 2;
		private const int EVENT_DATA_FILE_ADDED = 3;
		#endregion

		[Conditional("TRACE")]
		public static void LogCachedFileHit(string fileName)
		{
			//LogEvent(EVENT_CACHE_HIT, fileName);
		}

		[Conditional("TRACE")]
		public static void LogDataFileReloaded(string fileName)
		{
			//LogEvent(EVENT_DATA_FILE_RELOADED, fileName);
		}

		[Conditional("TRACE")]
		public static void LogDataFileAdded(string fileName)
		{
			//LogEvent(EVENT_DATA_FILE_ADDED, fileName);
		}
		private static void LogEvent(int eventId, params object[] parameters)
		{
			LoggerFactory.CreateLogger().LogEvent(LOG_CATEGORY_STRING, eventId, parameters);
		}
	}
}
