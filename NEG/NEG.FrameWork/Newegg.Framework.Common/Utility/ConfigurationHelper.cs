/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/19/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.IO;

namespace Newegg.Framework.Utility
{
	public class ConfigurationHelper
	{
		public static string GetConfigurationFile(string appSection)
		{
            if (System.Configuration.ConfigurationManager.AppSettings[appSection] != null)
            {
                string configFile = System.Configuration.ConfigurationManager.AppSettings[appSection];

                if (File.Exists(configFile))
                {
                    return configFile;
                }

                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile.Replace('/', '\\').TrimStart('\\'));
            }
            else
            {
                return "";
            }
		}
	}
}
