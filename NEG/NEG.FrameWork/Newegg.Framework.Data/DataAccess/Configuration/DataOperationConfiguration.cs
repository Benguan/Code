/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  05/03/2007 18:18:22
 * Usage:
 *
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newegg.Framework.DataAccess.Configuration
{
	[XmlRoot("dataOperations", Namespace = "http://www.newegg.com/website/DataOperation", IsNullable = false)]
	public class DataOperationConfiguration
	{
		private DataOperationCommand[] m_DataCommandList;

		/// <remarks/>
		[XmlElement("dataCommand")]
		public DataOperationCommand[] DataCommandList
		{
			get
			{
				return m_DataCommandList;
			}
			set
			{
				m_DataCommandList = value;
			}
		}

		/// <summary>
		/// returns a list of command names this object contains.
		/// </summary>
		/// <returns></returns>
		public IList<string> GetCommandNames()
		{
			if (DataCommandList == null || DataCommandList.Length == 0)
			{
				return new string[0];
			}

			List<string> result = new List<string>(DataCommandList.Length);

			for (int i = 0; i < DataCommandList.Length; i++)
			{
				result.Add(DataCommandList[i].Name);
			}
			return result;
		}
	}
}
