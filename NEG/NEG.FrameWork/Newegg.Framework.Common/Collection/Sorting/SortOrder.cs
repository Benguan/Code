/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/31/2006 15:24:29
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
	/// sort order for search result.
	/// </summary>
	public enum SortOrder
	{
		[XmlEnum("ASC")]
		Asc,
		[XmlEnum("DESC")]
		Desc,
	}
}
