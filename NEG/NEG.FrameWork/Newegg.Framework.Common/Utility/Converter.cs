/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  10/14/2006 08:48:56
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;

namespace Newegg.Framework.Utility
{
	public static class Converter
	{
		/// <summary>
		/// Converts the input value to an int32.
		/// If the input is null or cannot be converted to the target type, defaultValue is returned.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static int ToInt32(object input, int defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			int output;
			bool res = int.TryParse(input.ToString(), out output);
			return res ? output : defaultValue;
		}
		public static long ToInt64(object input, long defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			long output;
			bool res = long.TryParse(input.ToString(), out output);
			return res ? output : defaultValue;
		}

		public static decimal ToDecimal(object input, decimal defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			decimal output;
			bool res = decimal.TryParse(input.ToString(), out output);
			return res ? output : defaultValue;
		}

		public static DateTime ToDateTime(object input, DateTime defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			DateTime output;
			bool res = DateTime.TryParse(input.ToString(), out output);
			return res ? output : defaultValue;
		}

		public static long ToLong(object input, long defaultValue)
		{
			if (input == null)
			{
				return defaultValue;
			}
			long output;
			bool res = long.TryParse(input.ToString(), out output);
			return res ? output : defaultValue;
		}

		public static IList<T> ToList<T>(T[] array)
		{
			IList<T> listT = null;
			if (array != null && array.Length > 0)
			{
				listT = new List<T>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					T obj = array[i];
					if (obj == null)
					{
						continue;
					}

					listT.Add(obj);
				}
			}
			return listT;
		}

		public static bool ToBoolean(string value)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
