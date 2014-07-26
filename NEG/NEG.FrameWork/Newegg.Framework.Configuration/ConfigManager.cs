/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Thales Fu(Thales.L.Fu@newegg.com)
 * Create Date:  5/2/2012
 * Usage:
 *
 * RevisionHistory
 * Date         Author                  Description
 * 5/2/2012    Thales.L.Fu             Create.
 * 
*****************************************************************/
using Castle.Windsor;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Newegg.Framework.Configuration
{
    /// <summary>
    /// Newee configuration manager.
    /// </summary>
    /// <typeparam name="TConfig">Type of configuration.</typeparam>
    public static class ConfigManager<TConfig>
        where TConfig : class
    {
        /// <summary>
        /// Cache manager name.
        /// </summary>
        private const string CacheManagerName = "Configuration";

        /// <summary>
        /// Get configuration.
        /// </summary>
        /// <param name="fileName">Parameter of file name.</param>
        /// <returns>Object of configuration.</returns>
        public static TConfig GetConfiguration(string fileName)
        {
            TConfig result = GetConfigurationFromCache(fileName);

            if (result == null)
            {
                result = GetConfigurationFromFile(fileName);
                AddConfigurationToCache(fileName, result);
            }

            return result;
        }

        /// <summary>
        /// Get configuration from cache.
        /// </summary>
        /// <param name="fileName">Parameter of file name.</param>
        /// <returns>Object of configuration.</returns>
        private static TConfig GetConfigurationFromCache(string fileName)
        {
            TConfig result = default(TConfig);

            string cacheKey = GetCacheKey(fileName);

            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                ICacheManager cacheManager = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>(CacheManagerName);
                result = cacheManager.GetData(cacheKey) as TConfig;
            }

            return result;
        }

        /// <summary>
        /// Get configuration from file.
        /// </summary>
        /// <param name="fileName">Parameter of file name.</param>
        /// <returns>Object of configuration.</returns>
        private static TConfig GetConfigurationFromFile(string fileName)
        {
            TConfig result = default(TConfig);

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                IWindsorContainer container = new WindsorContainer(fileName);
                result = container.Resolve<TConfig>();
            }

            return result;
        }

        /// <summary>
        /// Get configuration from cache.
        /// </summary>
        /// <param name="fileName">Parameter of file name.</param>
        /// <param name="configuration">Parameter of configuration.</param>
        private static void AddConfigurationToCache(string fileName, object configuration)
        {
            string cacheKey = GetCacheKey(fileName);

            if (!string.IsNullOrWhiteSpace(cacheKey) && configuration != null)
            {
                ICacheManager cacheManager = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>(CacheManagerName);
                cacheManager.Add(cacheKey, configuration, CacheItemPriority.High, null, new FileDependency(fileName));
            }
        }

        /// <summary>
        /// Get cache key string.
        /// </summary>
        /// <param name="keyString">Parameter of cache key string.</param>
        /// <returns>Cache key.</returns>
        private static string GetCacheKey(string keyString)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(keyString))
            {
                result = keyString.Trim().ToUpper();
            }

            return result;
        }
    }
}
