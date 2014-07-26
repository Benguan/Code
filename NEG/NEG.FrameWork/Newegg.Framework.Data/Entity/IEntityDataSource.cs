/*****************************************************************
// Copyright (C) 2005-2006 Newegg Corporation
// All rights reserved.
// 
// Author:   Robert Wang (robert.q.wang@newegg.com)
// Create Date:  01/07/2006 14:53:56
// Usage:
//
// RevisionHistory
// Date         Author               Description
// 
*****************************************************************/

using System;
using System.Collections.Generic;

namespace Newegg.Framework.Entity
{
	/// <summary>
	/// represents a single row of data to populate an entity. 
	/// IEntityDataSource provides two functions:
	///		1. Iterate through all the column names in the row. this is done by the IEnumerable &lt;string&gt; interface.
	///			example:
	///				IEntityDataSource ds;
	///				foreach(string columnName in ds)
	///				{
	///					...
	///				}
	///		2. get the value of a specific field in the row. example:
	///				IEntityDataSource ds;
	///				int CategoryID = ds["CategoryID"];
	/// 
	///	the common pattern for using this interface is:
	///		IEntityDataSource ds;
	///		foreach(string columnName in ds)
	///		{
	///			object val = ds[columnName];
	///			// manipulate column name and the field value
	///			...
	///		}
	/// </summary>
	internal interface IEntityDataSource : IEnumerable<string>, IDisposable
	{
		/// <summary>
		/// returns the data contained in the specified column in the row
		/// </summary>
		/// <param name="columnName">column name in the row.</param>
		/// <returns>the value of the specified field in the row.</returns>
		object this[string columnName]
		{
			get;
		}

		object this[int index]
		{
			get;
		}

		/// <summary>
		/// indicates if the datasource contains the specified column
		/// </summary>
		/// <param name="columnName">column name</param>
		/// <returns>true if the specified column exists, false otherwise.</returns>
		bool ContainsColumn(string columnName);
	}
}
