/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  11/25/2006 14:01:19
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.Framework.Logging
{
    /// <summary>
    /// Log
    /// </summary>
    public interface ILogger
	{
		void LogEvent(string category, int eventId, params object[] parameters);
	}
}
