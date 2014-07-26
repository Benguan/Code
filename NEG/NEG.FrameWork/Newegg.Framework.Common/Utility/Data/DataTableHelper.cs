/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jason Huang (jason.j.huang@newegg.com)
 * Create Date:  11/16/2006 16:58:08
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System.Collections;

namespace Newegg.Framework.Utility.Data
{
	using System.Data;

	public static class DataTableHelper
	{
		/// <summary>
		/// Gets the name of the uniqued table columns by column.
		/// </summary>
		/// <param name="dataRowCollection">The data row collection.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <returns></returns>
		public static string GetUniquedTableColumnsByColumnName<T>(T dataRowCollection, string columnName) where T : ICollection
		{
			string tmpRet = ",";
			string tmp;

			foreach (DataRow row in dataRowCollection)
			{
				tmp = ",'" + row[columnName].ToString() + "',";
				if (tmpRet.LastIndexOf(tmp) == -1)
				{
					tmpRet += "'" + row[columnName].ToString() + "',";
				}
			}
			if (tmpRet.Length > 1)
			{
				return tmpRet.Remove(tmpRet.Length - 1, 1).Remove(0, 1);
			}
			return string.Empty;
		}

		/// <summary>
		/// Gets the name of the uniqued table columns count by column.
		/// </summary>
		/// <param name="dataRowCollection">The data row collection.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <returns></returns>
		public static int GetUniquedTableColumnsCountByColumnName<T>(T dataRowCollection, string columnName) where T : ICollection
		{
			int count = 0;
			string tmpString = ",";
			string tmp;

			foreach (DataRow row in dataRowCollection)
			{
				tmp = ",'" + row[columnName].ToString() + "',";
				if (tmpString.LastIndexOf(tmp) == -1)
				{
					tmpString += "'" + row[columnName].ToString() + "',";
					count++;
				}
			}
			return count;
		}

		/// <summary>
		/// Gets the table from data row array.
		/// </summary>
		/// <param name="rows">The rows.</param>
		/// <returns></returns>
		public static DataTable GetTableFromDataRowArray(DataRow[] rows)
		{
			if (rows.Length == 0)
			{
				return null;
			}
			DataTable tmpTable = rows[0].Table.Clone();
			foreach (DataRow row in rows)
			{
				tmpTable.ImportRow(row);
			}

			return tmpTable;
		}

		/// <summary>
		/// Determines whether [has rows in data table] [the specified table].
		/// </summary>
		/// <param name="table">The table.</param>
		/// <returns>
		/// 	<c>true</c> if [has rows in data table] [the specified table]; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasRowsInDataTable(DataTable table)
		{
			if (table == null || table.Rows.Count == 0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Determines whether [has rows in data table] [the specified table].
		/// </summary>
		/// <param name="table">The table.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="columnValue">The column value.</param>
		/// <returns>
		/// 	<c>true</c> if [has rows in data table] [the specified table]; otherwise, <c>false</c>.
		/// </returns>
		public static bool HasRowsInDataTable(DataTable table, string columnName, string columnValue)
		{
			if (table == null || table.Rows.Count == 0)
			{
				return false;
			}
			DataRow[] rows = table.Select(columnName + "='" + columnValue + "'");
			if (rows.Length > 0)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Gets the selectd rows from table.
		/// </summary>
		/// <param name="orginalTable">The orginal table.</param>
		/// <param name="selectedCondition">The selected condition.</param>
		/// <returns></returns>
		public static DataRow[] GetSelectdRowsFromTable(DataTable orginalTable, string selectedCondition)
		{
			return orginalTable.Select(selectedCondition);
		}

		/// <summary>
		/// Gets the scalar by column name from table.
		/// </summary>
		/// <param name="sourceTable">The source table.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="columnName">Name of the column.</param>
		/// <returns></returns>
		public static string GetScalarByColumnName(DataTable sourceTable, string filter, string columnName)
		{
			if (sourceTable == null)
			{
				return string.Empty;
			}
			try
			{
				if (string.IsNullOrEmpty(filter))
				{
					return sourceTable.Rows[0][columnName].ToString();
				}
				else
				{
					DataRow[] rows = sourceTable.Select(filter);
					if (rows.Length > 0)
					{
						return rows[0][columnName].ToString();
					}
				}
			}
			catch
			{
				return string.Empty;
			}
			return string.Empty;
		}
	}
}
