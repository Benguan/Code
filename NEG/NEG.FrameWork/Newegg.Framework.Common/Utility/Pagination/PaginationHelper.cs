/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jason Huang (jason.j.huang@newegg.com)
 * Create Date:  11/16/2006 16:45:06
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;

using Newegg.Framework.Utility.Data;

namespace Newegg.Framework.Utility.Pagination
{
	using System.Data;

	public static class PaginationHelper
	{
		#region internal class
		private class PaginationInfo
		{
			private const int DEFAULT_PAGE_SIZE = 10;
			private int m_CurrentPageIndex;
			private int m_PageCount;

			private int m_StartIndex;
			private int m_EndIndex;

			public int PageCount
			{
				get { return m_PageCount; }
			}
			public int StartIndex
			{
				get { return m_StartIndex; }
			}
			public int EndIndex
			{
				get { return m_EndIndex; }
			}
			public int CurrentPageIndex
			{
				get { return m_CurrentPageIndex; }
			}

			public PaginationInfo(int pageIndex, int pageSize, int itemCount)
			{
				//judge the pageSize input.
				if (pageSize <= 0)
				{
					pageSize = DEFAULT_PAGE_SIZE;
				}
				//adjust the pageIndex input.
				if (pageIndex <= 0)
				{
					pageIndex = 1;
				}

				//get pageCount info by input.
				int tempPageCount = itemCount / pageSize;
				int tempRemaination = itemCount % pageSize;
				if (tempRemaination == 0)
				{
					m_PageCount = tempPageCount;
				}
				else
				{
					m_PageCount = tempPageCount + 1;
				}

				//judge the pageSize agion by pageCount.
				if (pageIndex > m_PageCount)
				{
					pageIndex = m_PageCount;
				}

				m_StartIndex = (pageIndex - 1) * pageSize;

				if (pageIndex == m_PageCount)
				{
					m_EndIndex = m_StartIndex + (tempRemaination == 0 ? pageSize : tempRemaination);
				}
				else
				{
					m_EndIndex = m_StartIndex + pageSize;
				}
				if (m_EndIndex > m_StartIndex)
				{
					m_EndIndex--;
				}
				m_CurrentPageIndex = pageIndex;
			}
		}
		#endregion

		public static List<T> GetPaginationList<T>(List<T> sourceList, int pageIndex, int pageSize, out int pageCount)
		{
			pageCount = 0;
			if (sourceList == null || sourceList.Count == 0)
			{
				return sourceList;
			}
			PaginationInfo paginationInfo = new PaginationInfo(pageIndex, pageSize, sourceList.Count);

			if (paginationInfo.CurrentPageIndex < paginationInfo.PageCount)
			{
				sourceList.RemoveRange(paginationInfo.EndIndex + 1, sourceList.Count - paginationInfo.EndIndex - 1);
			}
			if (paginationInfo.StartIndex > 0)
			{
				sourceList.RemoveRange(0, paginationInfo.StartIndex);
			}
			pageCount = paginationInfo.PageCount;
			return sourceList;
		}

		public static DataTable GetCurrentPageTable(DataRow[] sourceRows, int pageIndex, int pageSize, out int pageCount)
		{
			DataTable tmpTable = DataTableHelper.GetTableFromDataRowArray(sourceRows);
			return GetCurrentPageTable(tmpTable, pageIndex, pageSize, out pageCount);
		}

		public static DataTable GetCurrentPageTable(DataTable orginalTable, int pageIndex, int pageSize, out int pageCount)
		{
			pageCount = 0;
			if (orginalTable == null || orginalTable.Rows.Count == 0)
			{
				return orginalTable;
			}
			PaginationInfo paginationInfo = new PaginationInfo(pageIndex, pageSize, orginalTable.Rows.Count);
			pageCount = paginationInfo.PageCount;

			int rowsCount = orginalTable.Rows.Count;
			//remove pre-item.
			if (paginationInfo.EndIndex < orginalTable.Rows.Count)
			{
				for (int i = rowsCount - 1; i > paginationInfo.EndIndex; i--)
				{
					orginalTable.Rows.RemoveAt(i);
				}
			}
			//remove next-item.
			if (paginationInfo.StartIndex > 0)
			{
				for (int i = paginationInfo.StartIndex - 1; i >= 0; i--)
				{
					orginalTable.Rows.RemoveAt(i);
				}
			}
			return orginalTable;
		}

		public static DataTable GetCurrentPageTable(DataTable orginalTable, string sort, int pageIndex, int pageSize, out int pageCount)
		{
			return GetCurrentPageTable(orginalTable, null, sort, pageIndex, pageSize, out pageCount);
		}

		public static DataTable GetCurrentPageTable(DataTable orginalTable, string keyColumn, string sort, int pageIndex, int pageSize, out int pageCount)
		{
			if (string.IsNullOrEmpty(keyColumn))
			{
				if (string.IsNullOrEmpty(sort))
				{
					return GetCurrentPageTable(orginalTable, pageIndex, pageSize, out pageCount);
				}
				else
				{
					return GetCurrentPageTable(orginalTable.Select(string.Empty, sort), pageIndex, pageSize, out pageCount);
				}
			}

			string keyColumns = DataTableHelper.GetUniquedTableColumnsByColumnName(orginalTable.Rows, keyColumn);
			List<string> tmpKeyList =
				ConvertArrayToList(keyColumns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

			List<string> currentPageKeyList = GetPaginationList(tmpKeyList, pageIndex, pageSize, out pageCount);
			string filter = string.Format("{0} IN ({1})", keyColumn, ConvertListToString(currentPageKeyList));

			return DataTableHelper.GetTableFromDataRowArray(orginalTable.Select(filter, sort));
		}

		private static List<T> ConvertArrayToList<T>(T[] arrayName)
		{
			List<T> tmpList = new List<T>();
			foreach (T item in arrayName)
			{
				tmpList.Add(item);
			}
			return tmpList;
		}

		private static string ConvertListToString(List<string> orginalList)
		{
			string ret = string.Empty;
			foreach (string item in orginalList)
			{
				ret += item + ",";
			}
			if (ret.EndsWith(","))
			{
				ret = ret.Remove(ret.Length - 1);
			}
			return ret;
		}
	}
}
