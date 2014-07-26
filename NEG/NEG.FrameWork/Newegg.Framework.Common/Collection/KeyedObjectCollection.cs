/*****************************************************************
// Copyright (C) 2005-2006 Newegg Corporation
// All rights reserved.
// 
// Author:   Allen Wang (Allen.G.Wang@Newegg.com)
// Create Date:  01/10/2006 14:32:36
// Usage:
//
// RevisionHistory
// Date         Author               Description
// 2006-09-06	Robert Wang			documentation
 * 2006-09-08	Robert Wang			change SortedDictionary to Dictionary
*****************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Newegg.Framework.Collection
{
	/// <summary>
	/// Represents a collection of IKeyedObject objects.
	/// </summary>
	/// <remarks>
	/// Note: the implementation of this class uses two classes, namely List and Dictionary, to allow
	///		both integer- and string- typed index access. This occupies more memory than usual collection class.
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	public class KeyedObjectCollection<TKey, TItem> : IKeyedObjectCollection<TKey, TItem> where TItem : IKeyedObject<TKey>
	{
		private Dictionary<TKey, TItem> entriesTable;
		private List<TItem> entries;
		private IComparer comparer;

		/// <summary>
		/// <para>Initializes a new instance of the <see cref="T"/> class.</para>
		/// </summary>
		public KeyedObjectCollection() : this(null)
		{
		}

		public KeyedObjectCollection(IEqualityComparer<TKey> equalityComaprer)
		{
			if (equalityComaprer == null)
			{
				if (typeof(TKey) == typeof(string))
				{
					entriesTable = new Dictionary<TKey, TItem>(new CaseInsensitiveStringEqualityComparer() as IEqualityComparer<TKey>);
				}
				else
				{
					entriesTable = new Dictionary<TKey, TItem>();
				}
			}
			else
			{
				entriesTable = new Dictionary<TKey, TItem>(equalityComaprer);
			}
			entries = new List<TItem>();
			comparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
		}

		public TItem this[int index]
		{
			get { return (TItem)entries[index]; }
		}

		/// <summary>
		/// Returns null if specified key does not exist in the collection.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public TItem this[TKey key]
		{
			get 
			{
				return GetItemByKey(key);
			}
		}

		public TItem GetItemByKey(TKey key)
		{
			TItem val;
			entriesTable.TryGetValue(key, out val);
			return val;
		}

		public bool Contains(TKey key)
		{
			return entriesTable.ContainsKey(key);
		}

		#region ICollection<T> Members

		public void Add(TItem item)
		{
			entriesTable.Add(item.Key, item);
			entries.Add(item);
		}

		public void Clear()
		{
			entriesTable.Clear();
			entries.Clear();
		}

		public bool Contains(TItem item)
		{
			return entriesTable.ContainsKey(item.Key);
		}

		public void CopyTo(TItem[] array, int arrayIndex)
		{
			entriesTable.Values.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return entriesTable.Count; }
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public bool Remove(TItem item)
		{
			bool removed = false;
			try
			{
				entriesTable.Remove(item.Key);

				// remove from array
				for (int i = entries.Count - 1; i >= 0; i--)
				{
					TItem entry = (TItem)entries[i];
					if (comparer.Compare(item.Key, entry.Key) == 0)
					{
						entries.RemoveAt(i);
					}
				}

				removed = true;
			}
			catch
			{
				removed = false;
			}

			return removed;
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<TItem> GetEnumerator()
		{
			return entries.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return entries.GetEnumerator();
		}

		#endregion
	}
}
