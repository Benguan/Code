/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/26/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Xml.Serialization;

namespace Newegg.Framework.DataAccess
{
	/// <summary>
	/// configuration that contains the list of DataCommand configuration files.
	/// This class is for internal use only.
	/// </summary>
    [XmlRoot("dataCommandFiles", Namespace = "http://www.newegg.com/Website/DbCommandFiles")]
	public class ConfigDataCommandFileList
	{
		private DataCommandFile[] m_commandFiles;
		
		public class DataCommandFile
		{
			private string m_FileName;
			
			[XmlAttribute("name")]
			public string FileName
			{
				get { return m_FileName; }
				set { m_FileName = value; }
			}
		}

		[XmlElement("file")]
		public DataCommandFile[] FileList
		{
			get { return m_commandFiles; }
			set { m_commandFiles = value; }
		}
	}
}
