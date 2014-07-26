/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Cheney Hu (cheney.c.hu@newegg.com)
 * Create Date:  02/20/2009 13:28:32
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Cheney Hu (cheney.c.hu@newegg.com)
 * Create Date:  02/13/2009 14:00:56
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.Caching;
using System.Text;
using Newegg.Framework.Caches;

namespace Newegg.Framework.Globalization
{
    internal class ResourceLoader : IDisposable
    {
        #region fields

        private readonly string m_BaseName;
        private readonly string m_ResourcesDir;
        private readonly MemoryCache m_Cache;

        #endregion

        #region ctor

        public ResourceLoader(string baseName, string resourcesDir)
        {
            this.m_Cache = new MemoryCache(string.Concat("Resx", baseName));

            m_BaseName = baseName;
            if (string.IsNullOrEmpty(resourcesDir))
            {
                m_ResourcesDir = AppDomain.CurrentDomain.BaseDirectory;
                return;
            }
            DirectoryInfo directory = new DirectoryInfo(resourcesDir);
            if (directory.Exists)
            {
                m_ResourcesDir = directory.FullName;
            }
            else
            {
                m_ResourcesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourcesDir);
            }
        }

        #endregion

        #region LoadResource

        public ResourceSet LoadResource(string site, CultureInfo culture)
        {
            string key = MakeKey(site, culture);
            ResourceSet resource = m_Cache.Get(key) as ResourceSet;
            if (resource != null)
            {
                return resource;
            }

            string resourceFile = GetResourceFileName(site, culture);
            try
            {
                if (File.Exists(resourceFile))
                {
                    resource = new ResXResourceSet(resourceFile);
                }
            }
            catch (Exception ex)
            {
                throw new ResourceLoadingException(string.Format("load resource file {0} failed", resourceFile), ex);
            }
            if (resource == null)
            {
                resource = new EmptyResourceSet();
            }

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.ChangeMonitors.Add(new PathsChangeMonitor(new List<string> { resourceFile }));
            m_Cache.Add(key, resource, policy);
            return resource;
        }


        #endregion

        #region helpful methods

        private string MakeKey(string site, CultureInfo culture)
        {
            return string.Concat(culture.Name, "@", site);
        }

        public string GetResourceFileName(string site, CultureInfo culture)
        {
            return GetResourceFileName(site, culture, ResourceFileType.resx);
        }

        public string GetResourceFileName(string site, CultureInfo culture, ResourceFileType fileType)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(m_BaseName);
            if (!string.IsNullOrEmpty(site))
            {
                builder.Append('.');
                builder.Append(site);
            }
            if (!culture.Equals(CultureInfo.InvariantCulture))
            {
                builder.Append('.');
                builder.Append(culture.Name);
            }
            builder.Append(".");
            builder.Append(fileType.ToString());
            return Path.Combine(m_ResourcesDir, builder.ToString());
        }

        private class EmptyResourceSet : ResourceSet
        {
            public override string GetString(string name, bool ignoreCase)
            {
                return null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.m_Cache.Dispose();
        }

        #endregion

        private class ResourceLoadingException : ApplicationException
        {
            public ResourceLoadingException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
    }
}
