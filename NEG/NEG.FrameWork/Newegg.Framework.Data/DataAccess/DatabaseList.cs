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
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Newegg.Framework.DataAccess
{
	/*
	 todo: 
		currently xml serializer only support public class. whereas this class 
		should be internal only.
		to solve this issue, a dedicated helpclass can be added that uses DOM to deserialize 
		an instance of this class.
	 */
	[XmlRoot("databaseList", Namespace = "http://www.newegg.com/Website/DatabaseList")]
	public class DatabaseList
	{
		private DatabaseGroup[] m_DatabaseGroups;

		[XmlElement("dbGroup")]
		public DatabaseGroup[] DatabaseGroups
		{
			get { return m_DatabaseGroups; }
			set { m_DatabaseGroups = value; }
		}
	}
	public class DatabaseGroup
	{
		private string m_Name;

		[XmlAttribute("name")]
		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}
		private DatabaseInstance[] m_DatabaseInstances;

		[XmlElement("database")]
		public DatabaseInstance[] DatabaseInstances
		{
			get { return m_DatabaseInstances; }
			set { m_DatabaseInstances = value; }
		}
	}
}
