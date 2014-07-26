/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/06/2006 17:18:36
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Concurrent;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using Newegg.Framework.Entity;
using Newegg.Framework.Utility;
using Newegg.Framework.XmlAccess.Configuration;
using Newegg.Framework.Collection;
using Newegg.Framework.Globalization;
using Newegg.Framework.Threading;

namespace Newegg.Framework.XmlAccess
{
    public static class XmlDataManager
    {
        private static FileSystemWatcher s_DataFileWatcher;
        private static FileSystemChangeEventHandler s_FileChangeHandler;
        private static Dictionary<string, DataTable> s_DataTableCache;

        /// <summary>
        /// Field of objects cache;
        /// </summary>
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, object>> objectCache;

        /// <summary>
        /// Field of table locker.
        /// </summary>
        private static object tableLocker;

        //add default xml data folder;
        private static string m_DefaultXmlDataFolder = (HttpContext.Current != null) ? HttpContext.Current.Server.MapPath(ConfigurationManager.XmlAccessConfiguration.DefaultXmlDataFolder) : Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.XmlAccessConfiguration.DefaultXmlDataFolder.Replace("/", "\\").TrimStart('~').TrimStart('\\'));
        private const int FILE_CHANGE_NOTIFICATION_INTERVAL = 500;

        static XmlDataManager()
        {
            tableLocker = new object();

            // create cache
            s_DataTableCache = new Dictionary<string, DataTable>(new CaseInsensitiveStringEqualityComparer());
            objectCache = new ConcurrentDictionary<string, ConcurrentDictionary<string, object>>(new CaseInsensitiveStringEqualityComparer());

            // setup folder monitor
            s_FileChangeHandler = new FileSystemChangeEventHandler(FILE_CHANGE_NOTIFICATION_INTERVAL);
            s_FileChangeHandler.ActualHandler += new FileSystemEventHandler(OnFileChanged);

            s_DataFileWatcher = new FileSystemWatcher(m_DefaultXmlDataFolder);
            s_DataFileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            s_DataFileWatcher.Changed += new FileSystemEventHandler(s_FileChangeHandler.ChangeEventHandler);
            s_DataFileWatcher.EnableRaisingEvents = true;
        }

        private static void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            string xmlDataFile = e.Name;
            if (s_DataTableCache.ContainsKey(xmlDataFile))
            {
                DataTable dt = XmlLoader.LoadDataTable(xmlDataFile);
                if (dt == null)
                {
                    return;
                }
                lock (tableLocker)
                {
                    s_DataTableCache[xmlDataFile] = dt;
                }

                //XmlDataManagerLogger.LogDataFileReloaded(xmlDataFile);
            }

            ConcurrentDictionary<string, object> cachedItem;
            objectCache.TryRemove(xmlDataFile, out cachedItem);
        }

        public static DataTable GetDataTable(string xmlFileName)
        {
            if (xmlFileName.Contains(StringResource.ThreadStorage_Value_MultiLang_PlaceHolder))
            {
                xmlFileName = xmlFileName.Replace(StringResource.ThreadStorage_Value_MultiLang_PlaceHolder, ResourceProfile.CurrentLanguageCode);
            }

            if (!s_DataTableCache.ContainsKey(xmlFileName))
            {
                DataTable dt;
                dt = XmlLoader.LoadDataTable(xmlFileName);
                if (dt == null)
                {
                    return null;
                }
                lock (tableLocker)
                {
                    if (!s_DataTableCache.ContainsKey(xmlFileName))
                    {
                        Dictionary<string, DataTable> newCache = new Dictionary<string, DataTable>(s_DataTableCache,
                            new CaseInsensitiveStringEqualityComparer());
                        newCache[xmlFileName] = dt;
                        s_DataTableCache = newCache;
                    }
                }
                return dt;
            }
            else
            {
                return s_DataTableCache[xmlFileName];
            }
        }

        public static List<T> GetEntityList<T>(string xmlFileName) where T : class, new()
        {
            xmlFileName = GetFinialXmlFileName(xmlFileName);

            return objectCache.GetOrAdd(xmlFileName, k => new ConcurrentDictionary<string, object>()).GetOrAdd("L", k => LoadEntityList<T>(xmlFileName)) as List<T>;
        }

        public static ILookup<TKey, TValue> GetEntityLookup<TKey, TValue>(string xmlFileName, string keySelectorName, Func<TValue, TKey> keySelector) where TValue : class, new()
        {
            xmlFileName = GetFinialXmlFileName(xmlFileName);

            string cacheKey = string.Concat("D", "-", keySelectorName);

            return objectCache.GetOrAdd(xmlFileName, k => new ConcurrentDictionary<string, object>()).GetOrAdd(cacheKey, k => GenerateLookup(xmlFileName, keySelector)) as ILookup<TKey, TValue>;
        }

        private static ILookup<TKey, TValue> GenerateLookup<TKey, TValue>(string xmlFileName, Func<TValue, TKey> keySelector) where TValue : class, new()
        {
            return LoadEntityList<TValue>(xmlFileName).ToLookup(keySelector);
        }

        /// <summary>
        /// Load entity list.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="xmlFileName">Parameter of xml file name.</param>
        /// <returns>Entity list.</returns>
        private static List<T> LoadEntityList<T>(string xmlFileName) where T : class, new()
        {
            DataTable dt = GetDataTable(xmlFileName);
            if (dt == null)
            {
                return new List<T>(0);
            }
            try
            {
                return EntityBuilder.BuildEntityList<T>(dt);
            }
            catch
            {
                return new List<T>(0);
            }
        }

        private static string GetFinialXmlFileName(string xmlFileName)
        {
            if (xmlFileName.Contains(StringResource.ThreadStorage_Value_MultiLang_PlaceHolder))
            {
                xmlFileName = xmlFileName.Replace(StringResource.ThreadStorage_Value_MultiLang_PlaceHolder, ResourceProfile.CurrentLanguageCode);
            }
            return xmlFileName;
        }
    }
}