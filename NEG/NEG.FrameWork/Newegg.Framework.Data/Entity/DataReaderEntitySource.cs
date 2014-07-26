/*****************************************************************
// Copyright (C) 2005-2006 Newegg Corporation
// All rights reserved.
// 
// Author:   Robert Wang (robert.q.wang@newegg.com)
// Create Date:  01/07/2006 15:23:19
// Usage:
//
// RevisionHistory
// Date         Author               Description
// 
*****************************************************************/

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Newegg.Framework.Entity
{
	/// <summary>
	/// Data source used to fill an entity. The underlying class is IDataReader.
	/// Note: remember to call Dispose() after using.
	/// </summary>
	internal class DataReaderEntitySource : IEntityDataSource
	{
		private class ReaderColumnNameEnumerator : IEnumerator<string>
		{
			private IEnumerator m_InternalEnumerator;

			public ReaderColumnNameEnumerator(IDataReader dr)
			{
				DataTable schemaTable = dr.GetSchemaTable();
				m_InternalEnumerator = schemaTable.Rows.GetEnumerator();
			}

			public string Current
			{
				get
				{
					DataRow row = m_InternalEnumerator.Current as DataRow;
					return row["ColumnName"] as string;
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get { return Current; }
			}

			public bool MoveNext()
			{
				return m_InternalEnumerator.MoveNext();
			}

			public void Dispose()
			{
				return;
			}

			public void Reset()
			{
				m_InternalEnumerator.Reset();
			}
		}

		private IDataReader m_DataReader;

		public DataReaderEntitySource(IDataReader dr)
		{
			m_DataReader = dr;
		}

		public object this[string columnName]
		{
			get { return m_DataReader[columnName]; }
		}

		public object this[int index]
		{
			get { return m_DataReader[index]; }
		}
		public IEnumerator<string> GetEnumerator()
		{
			return new ReaderColumnNameEnumerator(m_DataReader);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return new ReaderColumnNameEnumerator(m_DataReader);
		}

		public bool ContainsColumn(string columnName)
		{
			DataTable schemaTable = m_DataReader.GetSchemaTable();
			foreach (DataRow row in schemaTable.Rows)
			{
				if (string.Compare(row["ColumnName"].ToString().Trim(), columnName.Trim(), true) == 0)
				{
					return true;
				}
			}
			return false;
		}

		public void Dispose()
		{
			if (m_DataReader != null)
			{
				m_DataReader.Dispose();
			}
		}
	}
}
