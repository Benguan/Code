/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Jason Huang (Jason.j.huang@newegg.com)
 * Create Date:  07/22/2008 13:17:28
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System.Xml.Serialization;
using System.Collections.Generic;

namespace Newegg.Framework.XmlAccess.Configuration
{
	public class AlternateXmlDataFolderCollection
	{
		private List<string> m_Folders;

		[XmlElement("folder")]
		public List<string> Folders
		{
			get { return m_Folders; }
			set { m_Folders = value; }
		}
	}
}