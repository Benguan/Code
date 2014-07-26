/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  05/03/2007 17:38:02
 * Usage:
 *
*****************************************************************/

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace Newegg.Framework.DataAccess.Configuration
{
	public class DataOperationParameter
	{
		private string m_Name;

		private DbType m_DbType;

		private ParameterDirection m_Direction;

		private int m_Size;

        private Byte m_Scale;

		public DataOperationParameter()
		{
			m_Direction = ParameterDirection.Input;
			m_Size = -1;
		}

		/// <remarks/>
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		/// <remarks/>
		[XmlAttribute("dbType")]
		public DbType DbType
		{
			get
			{
				return m_DbType;
			}
			set
			{
				m_DbType = value;
			}
		}

		/// <remarks/>
		[XmlAttribute("direction")]
		public ParameterDirection Direction
		{
			get
			{
				return m_Direction;
			}
			set
			{
				m_Direction = value;
			}
		}

		/// <remarks/>
		[XmlAttribute("size")]
		public int Size
		{
			get
			{
				return m_Size;
			}
			set
			{
				m_Size = value;
			}
		}

        /// <remarks/>
        [XmlAttribute("scale")]
        public Byte Scale
        {
            get
            {
                return m_Scale;
            }
            set
            {
                m_Scale = value;
            }
        }


		public DbParameter GetDbParameter()
		{
			// use parameterless constructor so that SqlDbType is avoided.
			SqlParameter param = new SqlParameter();
			param.ParameterName = Name;
			param.DbType = DbType;
			param.Direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), Direction.ToString());
			// the default is -1, specified in the schema
			if (Size != -1)
			{
				param.Size = Size;
			}
            if (Scale != 0)
            {
                param.Scale = Scale;
            }
			return param;
		}
	}
}
