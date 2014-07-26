/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Dolphin Zhang (Dolphin.F.Zhang at Newegg.com)
 * Create Date:  03/09/2007
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections;

using Newegg.Framework.Globalization;
using Newegg.Framework.Threading;

namespace Newegg.Framework.Utility
{
	public static class StringHelper
	{
		public static bool IsNullOrEmpty(string input)
		{
			return string.IsNullOrEmpty(input) || input.Trim() == string.Empty;
		}

		/// <summary>
		/// Gets the left string.
		/// </summary>
		/// <param name="description">The description.</param>
		/// <param name="leftLength">Length of the left.</param>
		/// <returns></returns>
		public static string GetLeftString(string description, int leftLength)
		{
			if (IsNullOrEmpty(description) || description.Length <= leftLength)
			{
				return description;
			}
			return description.Substring(0, leftLength);
		}
		
		/// <summary>
		/// Gets the right string.
		/// </summary>
		/// <param name="description">The description.</param>
		/// <param name="leftLength">Length of the left.</param>
		/// <returns></returns>
		public static string GetRightString(string description, int leftLength)
		{
			if (IsNullOrEmpty(description) || description.Length <= leftLength)
			{
				return description;
			}
			return description.Substring(description.Length - leftLength);
		}

		/// <summary>
		/// Trims the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>return trim value,if value can convert to string;otherwise,return null.</returns>
		public static string Trim(object value)
		{
			string convertible = Convert.ToString(value);
			if (string.IsNullOrEmpty(convertible))
			{
				return convertible;
			}
			return convertible.Trim();
		}

		public static string ConvertString(string description, string defaultValue)
		{
			if (IsNullOrEmpty(description))
			{
				return defaultValue;
			}
			return description;
		}

		public static string ConvertString(string description)
		{
			if (IsNullOrEmpty(description))
			{
				return "";
			}
			return description;
		}

		public static string XmlDecode(string data)
		{
			if (IsNullOrEmpty(data))
			{
				return string.Empty;
			}
			return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(data, "&apos;", "'", RegexOptions.IgnoreCase), "&quot;", "\"", RegexOptions.IgnoreCase), "&lt;", "<", RegexOptions.IgnoreCase), "&gt;", ">", RegexOptions.IgnoreCase), "&amp;", "&", RegexOptions.IgnoreCase);
		}

		public static string XmlEncode(string data)
		{
			if (IsNullOrEmpty(data))
			{
				return string.Empty;
			}
			return data.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("\"", "&quot;").Replace("'", "&apos;");
		}

		public static XmlNode GetSingleNode(XmlDocument input, string xpath)
		{
			return GetSingleNode(input, xpath, null);
		}

		public static XmlNode GetSingleNode(XmlDocument input, string xpath, XmlNamespaceManager nsmgr)
		{
			XmlNode m_return = null;
			try
			{
				if (nsmgr == null)
				{
					m_return = input.SelectSingleNode(xpath);
				}
				else
				{
					m_return = input.SelectSingleNode(xpath, nsmgr);
				}
			}
			catch
			{
			}

			return m_return;
		}
		
		/// <summary>
		/// Filters the blank control string.
		/// </summary>
		/// <param name="val">The val.</param>
		/// <returns></returns>
		public static string FilterBlankControlString(string val)
		{
			if (string.IsNullOrEmpty(val))
			{
				return string.Empty;
			}
			return val.Replace("\r", "").Replace("\n", "").Replace("\t", "");
		}
		
		/// <summary>
		/// Gets the separation string by comma.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static string GetSeparationStringByComma(ICollection values)
		{
			if (values == null || values.Count == 0)
			{
				return string.Empty;
			}
			string ret = string.Empty;
			foreach (object item in values)
			{
				if (item == null)
				{
					continue;
				}
				ret += item.ToString() + ",";
			}
			return string.IsNullOrEmpty(ret) ? ret : ret.Substring(0, ret.Length - 1);
		}
		
		/// <summary>
		/// Gets the separation string by comma and single quotes.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public static string GetSeparationStringByCommaAndSingleQuotes(ICollection values)
		{
			if (values == null || values.Count == 0)
			{
				return string.Empty;
			}
			string ret = string.Empty;
			foreach (object item in values)
			{
				if (item == null)
				{
					continue;
				}
				ret += "'" + item.ToString() + "',";
			}
			return string.IsNullOrEmpty(ret) ? ret : ret.Substring(0, ret.Length - 1);
		}

		/// <summary>
		/// get the string by the current language code
		/// </summary>
		/// <param name="defaultString">the default string.not include {Lang} tab.</param>
		/// <param name="multiLangString">the multilang format string,include {Lang} tab.</param>
		/// <returns>the formated string</returns>
		public static string MultiLangFormatString(string defaultString, string multiLangString)
		{
			if (ResourceProfile.IsEnglishLanguage)
			{
				return defaultString;
			}

			if (string.IsNullOrEmpty(multiLangString))
			{
				return multiLangString;
			}

			return multiLangString.Replace(StringResource.ThreadStorage_Value_MultiLang_PlaceHolder, ResourceProfile.CurrentLanguageCode);
		}
	}
}
