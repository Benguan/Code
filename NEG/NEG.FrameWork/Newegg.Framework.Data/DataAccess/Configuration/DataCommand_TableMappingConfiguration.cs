/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Charlie Yuan (Charlie.J.Yuan@newegg.com)
 * Create Date:  2010-3-15 10:20:50
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using Newegg.Framework.Configuration;

namespace Newegg.Framework.DataAccess.Configuration
{
    public class DataCommand_TableMappingConfiguration : ConfigurationManagerBase
    {
        #region
        internal const string WebConfig_Key_WebsiteInvariant = "DataCommand_TableMapping";
        private static DataCommand_TableMappingConfiguration m_Current;
        #endregion

        internal static DataCommand_TableMappingConfiguration Current
        {
            get
            {
                if (m_Current == null)
                {
                    m_Current = new DataCommand_TableMappingConfiguration();
                }
                return m_Current;
            }
        }

        public DataCommand_TableMappingConfig DataCommand_TableMappingConfig
        {
            get { return GetFromCache<DataCommand_TableMappingConfig>(WebConfig_Key_WebsiteInvariant); }
        }
    }
}
