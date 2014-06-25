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

using System.Xml.Serialization;

namespace Newegg.Website.Configuration
{
    [XmlRoot("bizUIConfig", Namespace = "http://www.newegg.com/Website")]
    public class BizConfiguration
    {
        #region fields

        private int maxItemNumuber;

        #endregion

        #region properties

        [XmlElement("MaxItemNumuber")]
        public int MaxItemNumber
        {
            get { return maxItemNumuber; }
            set { maxItemNumuber = value; }
        }

        #endregion

    }
}
