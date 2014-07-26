/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/29/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Data;

namespace Newegg.Framework.Entity
{
	public class DataMappingAttribute : Attribute
	{
		private string m_ColumnName;
		private DbType m_DbType;

		public DataMappingAttribute(string columnName, DbType dataType)
		{
			m_ColumnName = columnName;
			m_DbType = dataType;
		}

		private Type m_CaculatorType;

		public Type CaculatorType
		{
			get { return m_CaculatorType; }
			set { m_CaculatorType = value; }
		}

		public string ColumnName
		{
			get { return m_ColumnName; }
		}

		public DbType DbType
		{
			get { return m_DbType; }
		}
	}
}
