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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;

using Newegg.Framework.Threading;
using Newegg.Framework.Globalization;

namespace Newegg.Framework.Globalization
{
	public class NeweggResourceManager : ResourceManager, IDisposable
	{
		#region fields

		private ResourceLoader m_ResourceLoader;

		#endregion

		#region consts

		private const string DEFAULT_RESOURCES_DIR = "lang";
		private const string DEFAULT_STATIC_RESOURCES_DIR = "Themes\\2005\\Lang";
		#endregion

		#region ctor

		public NeweggResourceManager(string baseName)
			: this(baseName, DEFAULT_RESOURCES_DIR,ResourceFileType.resx)
		{
		}
		public NeweggResourceManager(string baseName, string resourceDir, ResourceFileType fileType)
		{
			if (string.IsNullOrEmpty(resourceDir))
			{
				switch (fileType)
				{
					case ResourceFileType.html:
					case ResourceFileType.aspx:
						resourceDir = DEFAULT_STATIC_RESOURCES_DIR;
						break;
					case ResourceFileType.resx:
					default:
						resourceDir = DEFAULT_RESOURCES_DIR;
						break;
				}
			}
			m_ResourceLoader = ResourcePool.GetLoader(baseName, resourceDir);
		}

		#endregion

		#region GetString

		public override string GetString(string name)
		{
			return GetString(name, null);
		}

		public override string GetString(string name, CultureInfo culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = ResourceProfile.CurrentCulture;
			}
			if (culture == null)
			{
				culture = CultureInfo.InvariantCulture;
			}
            //string siteName = GetSiteCountryCode();
            string siteName = GetSiteBizUnitName();
			string value = GetString(name, siteName, culture);
			if (value == null && !string.IsNullOrEmpty(siteName))
			{
				value = GetString(name, string.Empty, culture);
			}
			return value;
		}

		private string GetString(string name, string site, CultureInfo culture)
		{
			while (true)
			{
				ResourceSet resourceSet = m_ResourceLoader.LoadResource(site, culture);
				string value = resourceSet.GetString(name, true);
				if (value != null)
				{
					return value;
				}

				if (CultureInfo.InvariantCulture.Equals(culture))
				{
					break;
				}
				else
				{
					culture = culture.Parent;
				}
			}

			return null;
		}

        //private string GetSiteCountryCode()
        //{
        //    string countryCode = ResourceProfile.CurrentSite;
        //    if (countryCode == StringResource.ThreadStorage_Value_CountryCode_USA)
        //    {
        //        countryCode = string.Empty;
        //    }
        //    return countryCode;
        //}

        private string GetSiteBizUnitName()
        {
            string bizUnitName = ResourceProfile.CurrentSite;
            if (string.Compare(bizUnitName, StringResource.ThreadStorage_Value_RequestHost_Default, true) == 0)
            {
                bizUnitName = string.Empty;
            }
            return bizUnitName;
        }

		#endregion

		#region ResolveResource

		public static string ResolveResource(string resourcePath)
		{
			if (string.IsNullOrEmpty(resourcePath))
			{
				return null;
			}
			int lastDot = resourcePath.LastIndexOf('.');
			if (lastDot > 0)
			{
				string resourceModule = resourcePath.Substring(0, lastDot);
				string resourceKey = resourcePath.Substring(lastDot + 1);
				return new NeweggResourceManager(resourceModule).GetString(resourceKey);
			}
			return null;
		}
		#endregion

		#region Get HTML File path
		public static string ResolveStaticResource(string fileName)
		{
			return ResolveStaticResource(fileName, ResourceFileType.html, DEFAULT_STATIC_RESOURCES_DIR);
		}

		public static string ResolveStaticResource(string fileName, string resourceDir)
		{
			return ResolveStaticResource(fileName, ResourceFileType.html, resourceDir);
		}

		public static string ResolveStaticResource(string fileName, ResourceFileType fileType, string resourceDir)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				return null;
			}
			if (fileType == ResourceFileType.resx)
			{
				return null;
			}

			return new NeweggResourceManager(fileName, resourceDir, fileType).GetFullFileName(fileName, fileType);
		}
		#endregion

		#region get link
		public static string BuildLangUrl(string Link, string pagePath)
		{
			Link = Link.ToLower();
			pagePath = pagePath.ToLower();

			if (!string.IsNullOrEmpty(Link) && !string.IsNullOrEmpty(pagePath) && Link.IndexOf(pagePath) >= 0 && pagePath.IndexOf('.') >= 0)
			{
				string fileType = Path.GetExtension(pagePath).TrimStart('.');
				string fileName = Path.GetFileNameWithoutExtension(pagePath);
				string fileDir = Path.GetDirectoryName(pagePath).TrimStart('\\').TrimEnd('\\');

				ResourceFileType resourceFileType = ResourceFileType.aspx;
				if (fileType == ResourceFileType.aspx.ToString().ToLower())
				{
					resourceFileType = ResourceFileType.aspx;
				}
				else if (fileType == ResourceFileType.html.ToString().ToLower())
				{
					resourceFileType = ResourceFileType.html;
				}
				else
				{
					return Link;
				}

				string fileFullPath = new NeweggResourceManager(fileName, fileDir, resourceFileType).GetFullFileName(fileName, resourceFileType);

				if (!string.IsNullOrEmpty(fileFullPath))
				{
					Link = Link.Replace(pagePath.Substring(pagePath.LastIndexOf('/') + 1), fileFullPath.Substring(fileFullPath.LastIndexOf('\\') + 1));
				}
			}
			return Link;
		}
		#endregion

		public string GetFullFileName(string name, ResourceFileType fileType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			CultureInfo culture = ResourceProfile.CurrentCulture;

			if (culture == null || culture.Name == "en-US")
			{
				culture = CultureInfo.InvariantCulture;
			}

			string siteName = GetSiteBizUnitName();

			string fullFileName = m_ResourceLoader.GetResourceFileName(siteName, culture, fileType);

			if (!File.Exists(fullFileName) && !string.IsNullOrEmpty(siteName))
			{
				fullFileName = m_ResourceLoader.GetResourceFileName(string.Empty, culture, fileType);
			}
			return fullFileName;
		}

		#region IDisposable Members

		public void Dispose()
		{
			//ResourcePool.Release(m_ResourceLoader);
		}

		#endregion
	}
}
