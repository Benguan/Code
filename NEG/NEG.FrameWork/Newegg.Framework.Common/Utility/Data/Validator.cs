/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jason Huang (jason.j.huang@newegg.com)
 * Create Date:  02/09/2007 08:20:18
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;

namespace Newegg.Framework.Utility.Data
{
	public static class Validator
	{
		public static bool IsNumeric(string inputValue)
		{
			if (string.IsNullOrEmpty(inputValue))
			{
				return false;
			}
			foreach (char c in inputValue)
			{
				if (!Char.IsNumber(c))
				{
					return false;
				}
			}
			return true;
		} 
	}
}
