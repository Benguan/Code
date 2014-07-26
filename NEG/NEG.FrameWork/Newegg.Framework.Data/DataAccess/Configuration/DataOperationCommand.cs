/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  05/03/2007 17:33:24
 * Usage:
 *
*****************************************************************/

using System;
using System.Data.Common;
using System.Xml.Serialization;
using System.Data;
using System.Collections.Generic;
using Newegg.Framework.Collection;
using Newegg.Framework.Utility;

namespace Newegg.Framework.DataAccess.Configuration
{
	public class DataOperationCommand
	{
		private string m_CommandText;

		private DataOperationParameters parametersField;

		private string m_Name;

		private string m_Database;

		private CommandType m_CommandType;

		private int m_TimeOut;

		public DataOperationCommand()
		{
			m_CommandType = CommandType.Text;
			m_TimeOut = 300;
		}

		/// <remarks/>
		[XmlElement("commandText")]
		public string CommandText
		{
			get
			{
				return m_CommandText;
			}
			set
			{
				m_CommandText = value;
			}
		}

		/// <remarks/>
		[XmlElement("parameters")]
		public DataOperationParameters Parameters
		{
			get
			{
				return parametersField;
			}
			set
			{
				parametersField = value;
			}
		}

		/// <remarks/>
		[XmlAttribute("name")]
		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
			}
		}

		/// <remarks/>
		[XmlAttribute("database")]
		public string Database
		{
			get
			{
				return m_Database;
			}
			set
			{
				m_Database = value;
			}
		}

		/// <remarks/>
		[System.ComponentModel.DefaultValueAttribute(CommandType.Text)]
		[XmlAttribute("commandType")]
		public CommandType CommandType
		{
			get
			{
				return m_CommandType;
			}
			set
			{
				m_CommandType = value;
			}
		}

		/// <remarks/>
		[System.ComponentModel.DefaultValueAttribute(300)]
		[XmlAttribute("timeOut")]
		public int TimeOut
		{
			get
			{
				return m_TimeOut;
			}
			set
			{
				m_TimeOut = value;
			}
		}

		/// <summary>
		/// returns a new instance of DataCommand this object represents
		/// </summary>
		/// <returns></returns>
		public DataCommand GetDataCommand()
		{
			DataCommand command = new DataCommand(Database, GetDbCommand());
			if (Parameters != null && ArrayUtils.HasLength(Parameters.ParameterGroupCollection))
			{
				foreach (DataOperationParameterGroup parameter in Parameters.ParameterGroupCollection)
				{
					command.ParameterGroupDictionary.Add(parameter.Name, parameter);
				}
			}
			return command;
		}

		/// <summary>
		/// returns a new instance of DbCommand this object represents
		/// </summary>
		/// <returns></returns>
		private DbCommand GetDbCommand()
		{
			DbCommand cmd = DbCommandFactory.CreateDbCommand();
			cmd.CommandText = CommandText.Trim();
			cmd.CommandTimeout = TimeOut;
			cmd.CommandType = (CommandType)Enum.Parse(typeof(CommandType), CommandType.ToString());
			// todo: populate cmd
			if (Parameters != null && Parameters.ParameterList != null && Parameters.ParameterList.Length > 0)
			{
				foreach (DataOperationParameter param in Parameters.ParameterList)
				{
					cmd.Parameters.Add(param.GetDbParameter());
				}
			}
			return cmd;
		}
	}
}
