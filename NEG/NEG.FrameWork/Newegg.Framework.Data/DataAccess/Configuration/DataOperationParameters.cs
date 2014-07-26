/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jason Huang (Jason.j.huang@newegg.com)
 * Create Date:  07/22/2008 10:26:44
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System.Xml.Serialization;

namespace Newegg.Framework.DataAccess.Configuration
{
	public class DataOperationParameters
	{
		private DataOperationParameter[] paramField;
		private DataOperationParameterGroup[] m_ParameterGroupCollection;

		[XmlElement("param")]
		public DataOperationParameter[] ParameterList
		{
			get
			{
				return paramField;
			}
			set
			{
				paramField = value;
			}
		}

		[XmlElement("paramGroup")]
		public DataOperationParameterGroup[] ParameterGroupCollection
		{
			get { return m_ParameterGroupCollection; }
			set { m_ParameterGroupCollection = value; }
		}
	}
}