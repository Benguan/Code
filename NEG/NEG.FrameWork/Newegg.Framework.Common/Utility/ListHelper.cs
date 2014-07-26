/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Dolphin Zhang (Dolphin.F.Zhang at Newegg.com)
 * Create Date:  03/09/2007
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.Framework.Utility
{
	public static class ListHelper
	{
		public static void CopyTo<T>(IList<T> from, IList<T> to)
		{
			if (from == null || to == null)
			{
				return;
			}

			foreach (T t in from)
			{
				to.Add(t);
			}
		}
	}
}
