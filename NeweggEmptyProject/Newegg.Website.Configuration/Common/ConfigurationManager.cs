/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Ben Guan (Ben.B.Guan@newegg.com)
 * Create Date:  06/26/2013
 * Usage:
 *
 * RevisionHistory
 * Date         Author               PageDescription
 * 
*****************************************************************/

using Newegg.Framework.Configuration;
using Newegg.Website.Configuration;

namespace Newegg.Website.Configuration.Common
{
    public class ConfigurationManager
    {
        #region ctor
        private static InternalConfiguration Config;

        static ConfigurationManager()
        {
            if (Config == null)
            {
                Config = InternalConfiguration.GetInstance();
            }
        }
        #endregion

        private class InternalConfiguration : ConfigurationManagerBase
        {
            #region GetInstance

            private static InternalConfiguration Config;

            public static InternalConfiguration GetInstance()
            {
                if (Config == null)
                {
                    Config = new InternalConfiguration();
                }
                return Config;
            }
            #endregion

            #region section name in the configuration file (web.config). !!! keep sync with web.config !!!

            private const string SECTION_NAME_BIZ_UI = "BizUIConfigurationFile";
            
            #endregion

            #region private filed

            private BizConfiguration bizConfiguration;

            #endregion

            #region properties

            public BizConfiguration BizConfiguration
            {
                get
                {
                    bizConfiguration = GetFromCache<BizConfiguration>(SECTION_NAME_BIZ_UI);
                    return bizConfiguration;
                }
                set { bizConfiguration = value; }
            }

            #endregion
        }

        public static BizConfiguration BizConfiguration
        {
            get { return Config.BizConfiguration; }
            set { Config.BizConfiguration = value; }
        }
    }
}
