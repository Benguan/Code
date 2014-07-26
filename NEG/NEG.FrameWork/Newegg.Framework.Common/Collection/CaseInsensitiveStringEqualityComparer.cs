/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/19/2006 13:28:18
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;

namespace Newegg.Framework.Collection
{
	public class CaseInsensitiveStringEqualityComparer : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return (string.Compare(x, y, true) == 0);
		}

		public int GetHashCode(string obj)
		{
			return obj.ToUpper().GetHashCode();
		}
	}
}
