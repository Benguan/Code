/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/14/2006 17:21:01
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace Newegg.Framework.DataAccess
{
	internal static class DbCommandFactory
	{
		public static DbCommand CreateDbCommand()
		{
			return new SqlCommand();
		}
	}
}
