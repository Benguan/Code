/*****************************************************************
// Copyright (C) 2005-2006 Newegg Corporation
// All rights reserved.
// 
// Author:	 Robert Wang(robert.q.wang@newegg.com)
// Create Date:  02/17/2006 
// Usage:
//
// RevisionHistory
// Date         Author               Description
// 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Newegg.Framework.Collection.Sorting
{
	/// <summary>
	/// generic comparer class that compares two objects against the give property name.
	/// if the type of the object's property does not support comparison (i.e. the 
	/// property's type does not implement IComparable), the two objects will be regarded as 
	/// being equal.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EntityComparer<T> : IComparer<T>
	{
		private SortInfo[] m_SortInfoArray;

		public EntityComparer(string sortBy, SortOrder order)
		{
			m_SortInfoArray = new SortInfo[1];
			m_SortInfoArray[0] = new SortInfo(sortBy, order);
		}

		public EntityComparer(SortInfo[] sortInfos)
		{
			m_SortInfoArray = sortInfos;
		}

		public int Compare(T x, T y)
		{
			// sort info not specified, no sorting is performed...
			if (m_SortInfoArray == null || m_SortInfoArray.Length < 1)
			{
				return 0;
			}

			// iterate through each sortinfo
			for (int i = 0; i < m_SortInfoArray.Length; i++)
			{
				int res = 0;
				PropertyInfo property = typeof(T).GetProperty(m_SortInfoArray[i].SortBy);

				// property not found or not comparable, no sorting for this sortinfo is performed.
				if (property == null || property.PropertyType.IsSubclassOf(typeof(IComparable)))
				{
					continue;
				}

				if (m_SortInfoArray[i].SortOrder == SortOrder.Asc)
				{
					res = ((IComparable)property.GetValue(x, null)).CompareTo(property.GetValue(y, null));
				}
				else
				{
					res = ((IComparable)property.GetValue(y, null)).CompareTo(property.GetValue(x, null));
				}

				// no furthur comparison is required if we can already tell the result
				if (res != 0)
				{
					return res;
				}
			}
			return 0;
		}
	}
}
