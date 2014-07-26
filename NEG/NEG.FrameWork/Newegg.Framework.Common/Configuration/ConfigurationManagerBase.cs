/*****************************************************************
// Copyright (C) 2005-2006 Newegg Corporation
// All rights reserved.
// 
// Author:	 Robert Wang(robert.q.wang@newegg.com)
// Create Date:  03/14/2006 18:13:48
// Usage:
//
// RevisionHistory
// Date         Author               Description
// 
*****************************************************************/

using System;
using System.Web.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Caching;

using Newegg.Framework.Utility;
using Newegg.Framework.Logging;
using System.Reflection;
using Newegg.Framework.Globalization;

namespace Newegg.Framework.Configuration
{
	/// <summary>
	/// Deserialize the configuration file into a runtime object, and monitors the configuration file and 
	/// reflects any change to the runtime object as well.
	/// </summary>
	/// <remarks>
	/// Note to the extenders:
	/// Future work:
	///		currently, configuration manager relies on System.Web.Caching.Cache to manage configuration objects.
	///		MS enterprise library caching component is not used because filedependecy checks file's date every time 
	///		an cache item is accessed and thus impose too much IO operations.
	///		System.Web.Caching.Cache supports many features and is therefore used here. This, however, restricts the
	///		future scalablity and usage scenarios of this component cause the consumer will have to be dependent on 
	///		System.Web.dll.
	///		Solution: implement a custome filedependy for MS enterprise library that uses FileSystemWatcher to 
	///		get notification of file changes rather than actively checking file dates and thus reduces IO operation.
	/// </remarks>
	public abstract class ConfigurationManagerBase
	{
		private Cache m_CacheManager;

		#region exception class for loading configuration file.

		[Serializable]
		public class LoadFileException : ApplicationException
		{
			private string m_FileName;
			private string m_TypeName;

			public LoadFileException(string typeName, string fileName)
			{
				m_FileName = fileName;
				m_TypeName = typeName;
			}

			public override string Message
			{
				get
				{
					return string.Format("Unable to load file {0} for type {1}", m_FileName, m_TypeName);
				}
			}
		}
		#endregion

		private object m_SyncObject;

		public ConfigurationManagerBase()
		{
			m_SyncObject = new object();
			m_CacheManager = CreateCache();
		}

		private Cache CreateCache()
		{
			return HttpRuntime.Cache;
		}

		#region cache manipulation
		/// <summary>
		/// if serialization fails, an exception is thrown.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="configFile"></param>
		/// <param name="needLog"></param>
		/// <returns></returns>
		/// <exception cref="LoadFileException"> when configuration file fails to load</exception>
		private T LoadConfiguration<T>(string cacheKey, string configFile, bool needLog)
			where T : class
		{
			T config;

			config = ObjectXmlSerializer.LoadFromXml<T>(configFile, needLog);
			if (config != null)
			{
				AddToCache(cacheKey, config, configFile, needLog);
			}
			else
			{
				throw new LoadFileException(typeof(T).Name, configFile);
			}

			return config;
		}


		/// <summary>
		/// Add configuration to cache
		/// </summary>
		/// <param name="key">section name defined in the web.config</param>
		/// <param name="value">configuration object</param>
		/// <param name="depedencyFile">config file</param>
		/// <param name="depedencyFile">need Log</param>
		private void AddToCache(string key, object value, string depedencyFile, bool needLog)
		{
			CacheItemRemovedCallback callBack = needLog ? new CacheItemRemovedCallback(LogCacheItemRemoved) : null;
			m_CacheManager.Add(key, value, new CacheDependency(depedencyFile), Cache.NoAbsoluteExpiration,
				Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, callBack);
		}

		/// <summary>
		/// Get configuration object from cache. If the underlying file changes, the object will be reloaded.
		/// </summary>
		/// <typeparam name="T">Type of configuration object</typeparam>
		/// <param name="key">file name of the configuration object</param>
		/// <param name="depedencyFile">need Log</param>
		/// <returns>configuration object</returns>
		/// <exception cref="LoadFileException"> when configuration file fails to load</exception>
		protected T GetFromCache<T>(string cacheKey, string key, bool needLog) where T : class
		{
			string realKey = cacheKey ?? key;
			T res = m_CacheManager[realKey] as T;
			if (res == null)
			{
				lock (m_SyncObject)
				{
					res = m_CacheManager[realKey] as T;
					if (res == null)
					{
						string configFile = ConfigurationHelper.GetConfigurationFile(key);
                        if (!string.IsNullOrEmpty(configFile))
                        {
                            res = LoadConfiguration<T>(realKey, configFile, needLog);
                        }
					}
				}
			}
			return res;
		}
		protected T GetFromCache<T>(string key) where T : class
		{
			return GetFromCache<T>(null, key, true);
		}
		protected T GetFromCache<T>(string cacheKey, string key) where T : class
		{
			return GetFromCache<T>(cacheKey, key, true);
		}
		#endregion // End of cache manipulation

		#region logging
		private const string CATEGORY_NAME = "Framework.Configuration";
		private const int EVENT_ID_CACHE_ITEM_REMOVED = 1;

		private static void LogCacheItemRemoved(string key, object obj, CacheItemRemovedReason reason)
		{
			//LoggerFactory.CreateLogger().LogEvent(CATEGORY_NAME, EVENT_ID_CACHE_ITEM_REMOVED, key, obj, reason);
		}
		#endregion
	}
}
