/*****************************************************************
 * Copyright (C) 2005-2006 Newegg Corporation
 * All rights reserved.
 * 
 * Author:   Jason Huang (jaosn.j.huang@newegg.com)
 * Create Date:  07/09/2008 15:12:41
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Newegg.Framework.Threading
{
	/// <summary>
	/// Implements <see cref="IThreadStorage"/> by using <see cref="HttpContext"/>
	/// </summary>
	public class HttpContextStorage : IThreadStorage
	{
		/// <summary>
		/// Retrieves an object with the specified name.
		/// </summary>
		/// <param name="name">The name of the item</param>
		/// <returns>The object in the context associated with the specified name or null if no object has been stored previously</returns>
		public object GetData(string name)
		{
			return HttpContext.Current.Items[name];
		}

		/// <summary>
		/// Stores a given object and associates it with the specified name.
		/// </summary>
		/// <param name="name">The object with which to associate the new item.</param>
		/// <param name="value">The object to store in the call context.</param>
		public void SetData(string name, object value)
		{
			HttpContext.Current.Items[name] = value;
		}

		/// <summary>
		/// Empties a data solt with the specified name.
		/// </summary>
		/// <param name="name">The name of the data slot to empty.</param>
		public void FreeNamedDataSlot(string name)
		{
			HttpContext.Current.Items.Remove(name);
		}
	}
}
