/*****************************************************************
 * Copyright (C) 2005-2006 Newegg Corporation
 * All rights reserved.
 * 
 * Author:   Jason Huang (jaosn.j.huang@newegg.com)
 * Create Date:  07/09/2008 15:12:41
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.Framework.Threading
{
	public class StringResource
	{
		public const string ThreadStorage_Key_RequestHost = "Request_Host";
		public const string ThreadStorage_Key_CurrencyExchangeRate = "CurrencyExchangeRate";
		public const string ThreadStorage_Value_RequestHost_Default = "USA";
		
		public const string ThreadStorage_Key_Is_Mobile_Device = "Key_Is_Mobile_Device";
		public const string ThreadStorage_Key_Is_Special_Mobile_Domain = "Key_Is_Special_Mobile_Domain";
		public const string ThreadStorage_Key_Language = "Key_Language";
		public const string ThreadStorage_Key_Is_Default_Language = "Key_Is_Default_Language";
		public const string ThreadStorage_Key_Default_Language_Code = "Default_Language_Code";
        public const string ThreadStorage_Key_EnableB2BInventorySeperation = "EnableB2BInventorySeperation";

		public const string ThreadStorage_Value_MultiLang_PlaceHolder = "#Lang#";
        public const string ThreadStorage_Value_DBPrefixUnicodeChar_PlaceHolder = "#DBPrefixN#";
        public const string ThreadStorage_Value_Unicode_DBPrefixChar = "N";
	    public const string ThreadStorage_Key_RegionCode = "Key_RegionCode";
        public const string ThreadStorage_Key_CurrencyCode = "Key_CurrencyCode";
        public const string ThreadStorage_Value_CurrencyCode_Default = "USD";
	    public const string ThreadStorage_Key_Is_Global_Shopping_Flow = "Key_Is_Global_Shopping_Flow";
	}
}
