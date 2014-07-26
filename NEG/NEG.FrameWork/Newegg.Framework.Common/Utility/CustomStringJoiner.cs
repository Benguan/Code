/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/19/2006 15:02:28
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
	public static class CustomStringJoiner
	{
		/// <summary>
		/// Joins the string.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <param name="delimiter">The delimiter.</param>
		/// <returns></returns>
		public static string JoinString<T>(IEnumerable<T> collection, string delimiter)
		{
			StringBuilder sb = new StringBuilder();
			int pos = 0;
			foreach (T t in collection)
			{
				if (t == null || StringHelper.IsNullOrEmpty(t.ToString()))
				{
					continue;
				}

				if (pos != 0)
				{
					sb.Append(delimiter);
				}
				sb.Append(t.ToString());
				pos++;
			}
			return sb.ToString();
		}
	}
}
