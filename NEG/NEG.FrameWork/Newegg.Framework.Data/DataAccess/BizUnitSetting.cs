/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jim Chen (Jim.J.Chen@newegg.com)
 * Create Date:  2010-1-26 19:23:30
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;

using Newegg.Framework.Configuration;
using Newegg.Framework.DataAccess.Configuration;

namespace Newegg.Framework.DataAccess
{
	public static class BizUnitSetting
	{
		#region Private class InternalConfiguration
		private class InternalConfiguration : ConfigurationManagerBase
		{
			#region section name in the configuration file (web.config). !!! keep sync with web.config !!!
			private const string SECTION_NAME_BIZ_UNIT = "BizUnit";

			#endregion

			private BizUnitConfig config;

			static private InternalConfiguration Current;

			#region get instance
			public static InternalConfiguration GetInstance()
			{
				if (Current == null)
				{
					Current = new InternalConfiguration();
				}
				return Current;
			}
			#endregion

			#region BizUnit Configuration
			public BizUnitConfig BizUnitConfig
			{
				get
				{
					//For performance sake, we only load this object from cache when application start
					//MODIFY THE FOLLOWING WITH CAUTION!!
					//By Leon Ma @ 2009-7-11
					if (config == null)
					{
						config = GetFromCache<BizUnitConfig>(SECTION_NAME_BIZ_UNIT);
					}
					return config;
				}
			}
			#endregion
		}

		#endregion

		private static InternalConfiguration Config;
		static BizUnitSetting()
		{
			Config = InternalConfiguration.GetInstance();
		}

		#region Get BizUnit Configuration
		public static BizUnitConfig BizUnitConfig
		{
			get { return Config.BizUnitConfig; }
		}
		#endregion

	}
}
