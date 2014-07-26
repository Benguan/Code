/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/24/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               PageDescription
 * 
*****************************************************************/

using System;
using System.Configuration;

using Newegg.Framework.Configuration;

namespace Newegg.Framework.XmlAccess.Configuration
{
	/// <summary>
	/// Provides configuraton info for the newegg site.
	/// </summary>
	internal static class ConfigurationManager
	{
		#region class definition for InternalConfiguration
		/// <summary>
		/// this is the adaptee class that implemented the actual property interfaces.
		/// all properties are instance properties.
		/// </summary>
		private class InternalConfiguration : ConfigurationManagerBase
		{
			#region section name in the configuration file (web.config). !!! keep sync with web.config !!!
			private const string SECTION_XMLACCESS_CONFIG = "XMLAccessConfigFile";
			private const string CACHEKEY_SECTION_XMLACCESS_CONFIG = "Framework.XmlAccess" + SECTION_XMLACCESS_CONFIG;
			#endregion

			static private InternalConfiguration Config;

			public static InternalConfiguration GetInstance()
			{
				if (Config == null)
				{
					Config = new InternalConfiguration();
				}
				return Config;
			}

			public XmlAccessConfiguration XmlAccessConfiguration
			{
				get { return GetFromCache<XmlAccessConfiguration>(CACHEKEY_SECTION_XMLACCESS_CONFIG, SECTION_XMLACCESS_CONFIG); }
			}
		}
		#endregion // class definition for InternalConfiguration

		private static InternalConfiguration Config;

		static ConfigurationManager()
		{
			Config = InternalConfiguration.GetInstance();
		}

		public static XmlAccessConfiguration XmlAccessConfiguration
		{
			get { return Config.XmlAccessConfiguration; }
		}
	}
}
