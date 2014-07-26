/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  09/05/2006 09:59:03
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Diagnostics;
using System.Text;

using Newegg.Framework.Logging;

namespace Newegg.Framework.Entity
{
	internal static class EntityBuilderLogger
	{
		private const string LogCategory = "Framework.EntityBuilder";
		#region event ids
		private const int GetMoneyCalcaulator = 1;	// not logged.
		private const int GetPropertyBindingInfo = 2; // not logged.
		#endregion

		[Conditional("TRACE")]
		public static void LogAddTypeInfo(Type type)
		{
			//LogEvent(AddTypeInfo, type.ToString());
		}

		[Conditional("TRACE")]
		public static void LogGetPropertyBindingInfoException(Type type, string columnName, Exception e)
		{
			LogEvent(GetPropertyBindingInfo, type.ToString(), columnName, e.ToString());
		}

		[Conditional("TRACE")]
		public static void LogGetMoneyCalculatorException(Type type, string columnName, Type calculatorType)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Get MoneyCalculator Exception.");
			sb.Append("Build Type:").AppendLine(type.FullName);
			sb.Append("Build Column:").AppendLine(columnName);
			sb.Append("Money Calculator Type:").AppendLine(calculatorType.FullName);
			LogEvent(GetMoneyCalcaulator, sb.ToString());
		}

		private static void LogEvent(int eventId, params object[] parameters)
		{
			LoggerFactory.CreateLogger().LogEvent(LogCategory, eventId, parameters);
		}
	}
}
