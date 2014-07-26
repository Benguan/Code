/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jim Chen (Jim.J.Chen@newegg.com)
 * Create Date:  2010-1-26 18:57:50
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newegg.Framework.DataAccess.Configuration
{
	[XmlRoot("bizUnitList",Namespace="http://www.newegg.com/Website")]
	public class BizUnitConfig
	{
		#region field
  		private List<BizUnitInfo> m_BizUnitInfoList;
		#endregion

		#region property
        [XmlElement("bizUnit")]
        public List<BizUnitInfo> BizUnitInfoList
        {
            get { return m_BizUnitInfoList; }
            set { m_BizUnitInfoList = value; }
        }
		#endregion

		public List<string> GetAvaliableLanguageCode(string defaultCode)
		{
			if (m_BizUnitInfoList == null || m_BizUnitInfoList.Count <= 0)
			{
				return null;
			}
			List<string> languageCodeList = new List<string>();
			foreach (BizUnitInfo unitInfo in m_BizUnitInfoList)
			{
				if (unitInfo.Languages != null && unitInfo.Languages.LanguageCodeList != null && unitInfo.Languages.LanguageCodeList.Count > 1)
				{
					unitInfo.Languages.LanguageCodeList.ForEach(new Action<string>(delegate(string languageCode)
					{
						if (!languageCodeList.Contains(languageCode) && !string.Equals(languageCode, defaultCode))
						{
							languageCodeList.Add(languageCode);
						}
					}));
				}
			}
			return languageCodeList;
		}
	}

	public class BizUnitInfo
	{
		#region fileds
		private string m_Name;
		private string m_CountryCode;
		private int m_CompanyCode;
		private string m_CurrencyCode;
		private Languages m_Languages;
		#endregion

		#region properties
		[XmlAttribute("name")]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		[XmlAttribute("countryCode")]
		public string CountryCode
		{
			get { return m_CountryCode; }
			set { m_CountryCode = value; }
		}

		[XmlAttribute("companyCode")]
		public int CompanyCode
		{
			get { return m_CompanyCode; }
			set { m_CompanyCode = value; }
		}

		[XmlAttribute("currencyCode")]
		public string CurrencyCode
		{
			get { return m_CurrencyCode; }
			set { m_CurrencyCode = value; }
		}

		[XmlElement("languages")]
		public Languages Languages
		{
			get { return m_Languages; }
			set { m_Languages = value; }
		}
		#endregion
	}

	public class Languages
	{
		private List<string> m_LanguageCodeList;

		[XmlElement("languageCode")]
		public List<string> LanguageCodeList
		{
			get { return m_LanguageCodeList; }
			set { m_LanguageCodeList = value; }
		}
	}

    public class EnableUnicodeDB
    {
        private bool m_Enable=false;

        [XmlAttribute("enable")]
        public bool Enable
        {
            get { return m_Enable; }
            set { m_Enable = value; }
        }
    }
}
