using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.Framework.Logging
{
	public class EmptyLogger : ILogger
	{
		#region ILogger Members

		public void LogEvent(string category, int eventId, params object[] parameters)
		{
			// do nothing
		}

		#endregion
	}
}
