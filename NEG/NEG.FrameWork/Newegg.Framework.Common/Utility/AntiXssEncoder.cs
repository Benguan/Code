/*****************************************************************
 * Copyright (C) 2005-2006 Newegg Corporation
 * All rights reserved.
 * 
 * Author:   Jason Huang (jaosn.j.huang@newegg.com)
 * Create Date:  07/02/2008 15:12:41
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System.Text;

namespace Newegg.Framework.Utility
{
	#region class summary
	/// <summary>
	/// <c>AntiXssEncoder</c> can be used to provide additional protection to ASP.NET Web-based applications against Cross-Site Scripting (XSS) attacks.
	/// <remarks>
	/// <para>
	/// To provide this protection, the library first defines a set of valid characters such as a-z and 
	/// A-Z and automatically encodes any characters not defined in that valid set (invalid characters or potential attack characters).
	/// This technique is commonly referred to as the principle of inclusions and can provide advantages over other techniques.
	/// Cross-site scripting (XSS) attacks exploit vulnerabilities in Web-based applications that fail to properly validate and/or encode input that is embedded in response data.  
	/// Malicious users can then inject client-side script into response data causing the unsuspecting user's browser to execute the script code.  
	/// The script code will appear to have originated from a trusted-site and may be able to bypass browser protection mechanisms such as security zones.
	/// These attacks are platform and browser independent, and can allow malicious users to perform malicious actions such as gaining unauthorized access 
	/// to client data like cookies or hijacking sessions entirely.
	/// <para />
	/// Simple steps that developers can take to prevent XSS attacks in their ASP.NET applications include 
	/// (see How To: Prevent Cross-Site Scripting in ASP.NET in the Patterns &amp; Practices series for more detail):
	/// <list type="bullet">
	///		<item>Validating and constraining input</item>
	///		<item>Encoding output</item>
	/// </list>
	/// </para>
	/// <para />
	/// <para>
	/// To properly use the Microsoft Anti-Cross Site Scripting Library to protect their ASP.NET Web-applications, developers need to:
	/// <list type="bullet">
	///		<item>Step 1: Review ASP.NET code that generates output</item>
	///		<item>Step 2: Determine whether output includes un-trusted input parameters</item>
	///		<item>Step 3: Determine the context which the un-trusted input is used as output</item>
	///		<item>Step 4: Encode output</item>
	/// </list>
	/// </para>
	/// <para>
	/// <b>Step 1: Review ASP.NET Code that Generates Output</b>
	/// XSS attacks are dependent on the ability of un-trusted input to be embedded as output, and so code that generates output must first be identified.  
	/// Some common vectors include calls to Response.Write and ASP &lt;% = calls.
	/// </para>
	/// <para>
	/// <b>Step 2: Determine if Output Could Contain Un-Trusted Input</b>
	/// Once the sections of code that generate output have been identified, they should be analysed to determined if the output may contain un-trusted input such as input from users or from some other un-trusted source.  If the output does contain un-trusted input then that un-trusted input will require encoding.  Some common sources of un-trusted input include:
	/// <list type="bullet">
	///		<item>Application variables</item>
	///		<item>Cookies</item>
	///		<item>Databases</item>
	///		<item>Form fields</item>
	///		<item>Query string variables</item>
	///		<item>Session variables</item>
	/// </list>
	/// If it is uncertain that the output may contain un-trusted input, then it is best to err on the side of caution and encode the output anyways.
	/// </para>
	/// <para>
	/// <b>Step 3: Determine Encoding Method to Use</b>
	/// <list type="table">
	///     <listheader>
	///         <term>Encoding Method</term>
	///         <term>Description</term>
	///     </listheader>
	///     <item>
	///         <description>HtmlEncode</description>
	///         <description>Encodes input strings for use in HTML</description>
	///     </item>
	///     <item>
	///         <description>HtmlAttributeEncode</description>
	///         <description>Encodes input strings for use in HTML attributes</description>
	///     </item>
	///     <item>
	///         <description>JavaScriptEncode</description>
	///         <description>Encodes input strings for use in JavaScript</description>
	///     </item>
	///     <item>
	///         <description>UrlEncode</description>
	///         <description>Encodes input strings for use in Universal Resource Locators (URLs)</description>
	///     </item>
	///     <item>
	///         <description>XmlEncode</description>
	///         <description>Encodes input strings for use in XML</description>
	///     </item>
	///     <item>
	///         <description>XmlAttributeEncode</description>
	///         <description>Encodes input strings for use in XML attributes</description>
	///     </item>
	/// </list>
	/// </para>
	/// <para>
	/// <b>Step 4: Encode Output</b>
	/// </para>
	/// </remarks>
	/// </summary>
	#endregion
	public static class AntiXssEncoder
	{
		// Fields
		private const string EmptyString_JavaScript = "''";
		private const string EmptyString_VBS = "\"\"";

		// Methods
		private static string EncodeHtml(string strInput)
		{
			if (strInput == null)
			{
				return null;
			}
			if (strInput.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder builder = new StringBuilder("", strInput.Length * 2);
			foreach (char ch in strInput)
			{
				if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch == ' ') || ((ch > '/') && (ch < ':'))) || (((ch == '.') || (ch == ',')) || ((ch == '-') || (ch == '_')))))
				{
					builder.Append(ch);
				}
				else
				{
					builder.Append("&#" + ((int)ch).ToString() + ";");
				}
			}
			return builder.ToString();
		}

		private static string EncodeHtmlAttribute(string strInput)
		{
			if (strInput == null)
			{
				return null;
			}
			if (strInput.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder builder = new StringBuilder("", strInput.Length * 2);
			foreach (char ch in strInput)
			{
				if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch > '/') && (ch < ':')) || (((ch == '.') || (ch == ',')) || ((ch == '-') || (ch == '_')))))
				{
					builder.Append(ch);
				}
				else
				{
					builder.Append("&#" + ((int)ch).ToString() + ";");
				}
			}
			return builder.ToString();
		}

		private static string EncodeJs(string strInput)
		{
			if (strInput == null)
			{
				return null;
			}
			if (strInput.Length == 0)
			{
				return "\"\"";
			}
			StringBuilder builder = new StringBuilder("\"", strInput.Length * 2);
			foreach (char ch in strInput)
			{
				if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch == ' ') || ((ch > '/') && (ch < ':'))) || (((ch == '.') || (ch == ',')) || ((ch == '-') || (ch == '_')))))
				{
					builder.Append(ch);
				}
				else if (ch > '\x007f')
				{
					builder.Append(@"\u" + TwoByteHex(ch));
				}
				else
				{
					builder.Append(@"\x" + SingleByteHex(ch));
				}
			}
			builder.Append("\"");
			return builder.ToString();
		}

		private static string EncodeUrl(string strInput)
		{
			if (strInput == null)
			{
				return null;
			}
			if (strInput.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder builder = new StringBuilder("", strInput.Length * 2);
			foreach (char ch in strInput)
			{
				if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch > '/') && (ch < ':')) || (((ch == '.') || (ch == '-')) || (ch == '_'))))
				{
					builder.Append(ch);
				}
				else if (ch > '\x007f')
				{
					builder.Append("%u" + TwoByteHex(ch));
				}
				else
				{
					builder.Append("%" + SingleByteHex(ch));
				}
			}
			return builder.ToString();
		}

		private static string EncodeVbs(string strInput)
		{
			if (strInput == null)
			{
				return null;
			}
			if (strInput.Length == 0)
			{
				return "\"\"";
			}
			StringBuilder builder = new StringBuilder("", strInput.Length * 2);
			bool flag = false;
			foreach (char ch in strInput)
			{
				if ((((ch > '`') && (ch < '{')) || ((ch > '@') && (ch < '['))) || (((ch == ' ') || ((ch > '/') && (ch < ':'))) || (((ch == '.') || (ch == ',')) || ((ch == '-') || (ch == '_')))))
				{
					if (!flag)
					{
						builder.Append("&\"");
						flag = true;
					}
					builder.Append(ch);
				}
				else
				{
					if (flag)
					{
						builder.Append("\"");
						flag = false;
					}
					builder.Append("&chrw(" + ((uint)ch).ToString() + ")");
				}
			}
			if ((builder.Length > 0) && (builder[0] == '&'))
			{
				builder.Remove(0, 1);
			}
			if (builder.Length == 0)
			{
				builder.Insert(0, "\"\"");
			}
			if (flag)
			{
				builder.Append("\"");
			}
			return builder.ToString();
		}

		private static string EncodeXml(string strInput)
		{
			return EncodeHtml(strInput);
		}

		private static string EncodeXmlAttribute(string strInput)
		{
			return EncodeHtmlAttribute(strInput);
		}

		public static string HtmlAttributeEncode(string s)
		{
			return EncodeHtmlAttribute(s);
		}

		public static string HtmlEncode(string s)
		{
			return EncodeHtml(s);
		}

		public static string HtmlEncodeUseLineBreak(string s)
		{
			return EncodeHtml(s).Replace("&#13;&#10;", "<br>").Replace("&#10;", "<br>").Replace("&#60;br&#62;", "<br>").Replace("&#60;br&#47;&#62;", "<br>");
		}

		public static string JavaScriptEncode(string s)
		{
			return EncodeJs(s);
		}

		private static string SingleByteHex(char c)
		{
			uint num = c;
			return num.ToString("x").PadLeft(2, '0');
		}

		private static string TwoByteHex(char c)
		{
			uint num = c;
			return num.ToString("x").PadLeft(4, '0');
		}

		public static string UrlEncode(string s)
		{
			return EncodeUrl(s);
		}

		public static string VisualBasicScriptEncode(string s)
		{
			return EncodeVbs(s);
		}

		public static string XmlAttributeEncode(string s)
		{
			return EncodeXmlAttribute(s);
		}

		public static string XmlEncode(string s)
		{
			return EncodeXml(s);
		}
	}
}
