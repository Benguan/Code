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

namespace Newegg.Framework.Threading
{
	/// <summary>
	/// Specifies the contract a strategy must be implement to store and 
	/// retrieve data that is specific to the executing thread.
	/// </summary>
	/// <remarks>
	/// All implementations of this interface must treat keys case-sensitive.
	/// </remarks>
	public interface IThreadStorage
	{
		/// <summary>
		/// Retrieves an object with the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">The name of the item.</param>
		/// <returns>
		/// The object in the current thread's context associated with the 
		/// specified <paramref name="name"/> or null if no object has been stored previously
		/// </returns>
		object GetData(string name);

		/// <summary>
		/// Stores a given object and associates it with the specified <paramref name="name"/>.
		/// </summary>
		/// <param name="name">The name with which to associate the new item.</param>
		/// <param name="value">The object to store in the current thread's context.</param>
		void SetData(string name, object value);

		/// <summary>
		/// Empties a data slot with the specified name.
		/// </summary>
		/// <remarks>
		/// If the object with the specified <paramref name="name"/> is not found, the method does nothing.
		/// </remarks>
		/// <param name="name">The name of the object to remove.</param>
		void FreeNamedDataSlot(string name);
	}
}
