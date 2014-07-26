/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/30/2006 09:32:36
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;

namespace Newegg.Framework.Entity
{
	public class ReferencedEntityAttribute : Attribute
	{
		private Type m_Type;
		private string m_Prefix;
		private string m_ConditionProperty;

		public ReferencedEntityAttribute(Type type)
		{
			m_Type = type;
		}
		public Type Type
		{
			get { return m_Type; }
		}

		public string Prefix
		{
			get { return m_Prefix; }
			set { m_Prefix = value; }
		}

		public string ConditionalProperty
		{
			get { return m_ConditionProperty; }
			set { m_ConditionProperty = value; }
		}
	}
}
