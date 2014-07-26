/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/31/2006 15:25:10
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Xml.Serialization;

namespace Newegg.Framework.Collection.Sorting
{
	/// <summary>
	/// sort information, including the property name and the sort order.
	/// This class is immutable.
	/// </summary>
	[XmlRoot("sortInfo")]
	public class SortInfo
	{
		private string m_SortBy;
		private SortOrder m_SortOrder;

		public SortInfo(string sortBy, SortOrder order)
		{
			m_SortBy = sortBy;
			m_SortOrder = order;
		}

		public SortInfo()
		{ 
		}

		[XmlAttribute("sortBy")]
		public string SortBy
		{
			get { return m_SortBy; }
			set { m_SortBy = value; }
		}

		[XmlAttribute("sortOrder")]
		public SortOrder SortOrder
		{
			get { return m_SortOrder; }
			set { m_SortOrder = value; }
		}
	}
}
