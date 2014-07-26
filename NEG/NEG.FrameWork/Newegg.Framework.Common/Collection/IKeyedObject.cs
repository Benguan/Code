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
// 2006-09-06	Robert Wang				Add documentation
 * 2006-10-10	Robert Wang				Add template support
*****************************************************************/

using System;
using System.Collections.Generic;

namespace Newegg.Framework.Collection
{
	/// <summary>
	/// Represents an object that owns a key and can be uniquely identified by that key in a collection.
	/// </summary>
	public interface IKeyedObject<T>
	{
		/// <summary>
		/// Gets the key that can uniquely identify the object.
		/// </summary>
		T Key
		{
			get;
		}
	}
}