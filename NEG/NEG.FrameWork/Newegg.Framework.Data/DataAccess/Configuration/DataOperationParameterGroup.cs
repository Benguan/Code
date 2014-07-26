/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jason Huang (Jason.j.huang@newegg.com)
 * Create Date:  07/22/2008 10:35:23
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System.Data;
using System.Xml.Serialization;

namespace Newegg.Framework.DataAccess.Configuration
{
	public class DataOperationParameterGroup
	{
		private string m_Name;
		private DbType m_DbType;
		private int m_Size;

		public DataOperationParameterGroup()
		{
			m_Size = -1;
		}

		[XmlAttribute("name")]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		[XmlAttribute("dbType")]
		public DbType DbType
		{
			get { return m_DbType; }
			set { m_DbType = value; }
		}

		[XmlAttribute("size")]
		public int Size
		{
			get { return m_Size; }
			set { m_Size = value; }
		}
	}
}