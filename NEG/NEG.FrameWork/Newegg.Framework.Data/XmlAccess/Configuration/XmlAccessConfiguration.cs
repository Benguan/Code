/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  11/21/2006 19:11:11
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Newegg.Framework.XmlAccess.Configuration
{
	[XmlRoot("xmlAccess", Namespace = "http://www.newegg.com/Website/Data")]
	public class XmlAccessConfiguration
	{
		private string m_DefaultXmlDataFolder;
		private AlternateXmlDataFolderCollection m_AlternateXmlDataFolders;

		[XmlElement("defaultXmlDataFolder")]
		public string DefaultXmlDataFolder
		{
			get { return m_DefaultXmlDataFolder; }
			set { m_DefaultXmlDataFolder = value; }
		}

		[XmlElement("alternateXmlDataFolders")]
		public AlternateXmlDataFolderCollection AlternateXmlDataFolders
		{
			get { return m_AlternateXmlDataFolders; }
			set { m_AlternateXmlDataFolders = value; }
		}
	}
}