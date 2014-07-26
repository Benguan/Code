/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/26/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.Framework.DataAccess
{
	/// <summary>
	/// An exception that occurred when there is no database specified in the configuration file.
	/// </summary>
	public class DatabaseNotSpecifiedException : Exception
	{
	}

	public class DataCommandFileNotSpecifiedException : Exception
	{
	}

	/// <summary>
	/// An exception that occurred when a DataCommand file does not exist or cannot be deserialized.
	/// </summary>
	public class DataCommandFileLoadException : Exception
	{
		public DataCommandFileLoadException(string fileName)
			: base("DataCommand file " + fileName + " not found or is invalid.")
		{
		}
	}
}
