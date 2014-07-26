/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Cheney Hu (cheney.c.hu@newegg.com)
 * Create Date:  02/28/2009 12:20:45
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System.Globalization;

using Newegg.Framework.Threading;

namespace Newegg.Framework.Globalization
{
	public class ResourceProfile
	{
		public static string CurrentSite
		{
            get { return LogicalThreadContext.GetData(StringResource.ThreadStorage_Key_RequestHost) as string; }
            set { LogicalThreadContext.SetData(StringResource.ThreadStorage_Key_RequestHost, value); }
		}

		public static CultureInfo CurrentCulture
		{
			get
			{
				CultureInfo culture = LogicalThreadContext.GetData(StringResource.ThreadStorage_Key_Language) as CultureInfo;
				if (culture == null)
				{
					culture = CultureInfo.InvariantCulture;
				}
				return culture;
			}
			set { LogicalThreadContext.SetData(StringResource.ThreadStorage_Key_Language, value); }
		}

		public static string CurrentLanguageCode
		{
			get
			{
				CultureInfo culture = ResourceProfile.CurrentCulture;
				if (culture == CultureInfo.InvariantCulture)
				{
					return "en";
				}
				return culture.TwoLetterISOLanguageName;
			}
		}

		public static bool IsEnglishLanguage
		{
			get
			{
				return string.Equals(CurrentLanguageCode, "en", System.StringComparison.OrdinalIgnoreCase);
			}
		}

		public static bool IsDefaultLanguage
		{
			get
			{
				return (bool)LogicalThreadContext.GetData(StringResource.ThreadStorage_Key_Is_Default_Language);
			}
		}
	}
}
