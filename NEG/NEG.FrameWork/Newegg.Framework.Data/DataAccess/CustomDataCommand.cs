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
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using Newegg.Framework.Globalization;
using Newegg.Framework.Utility;

namespace Newegg.Framework.DataAccess
{
	public class CustomDataCommand : DataCommand
	{
		#region ctor
		internal CustomDataCommand(string database)
			: base(database, DbCommandFactory.CreateDbCommand())
		{
		}
		internal CustomDataCommand(string database, bool supportTran)
			: this(database)
		{
			SupportTransaction = supportTran;
		}

		internal CustomDataCommand(string database, CommandType commandType)
			: this(database)
		{
			CommandType = commandType;
		}
		internal CustomDataCommand(string database, CommandType commandType, bool supportTran)
			: this(database, commandType)
		{
			SupportTransaction = supportTran;
		}

		internal CustomDataCommand(string database, CommandType commandType, string commandText)
			: this(database, commandType)
		{
			CommandText = commandText;
		}

		internal CustomDataCommand(string database, CommandType commandType, string commandText, bool supportTran)
			: this(database, commandType, commandText)
		{
			SupportTransaction = supportTran;
		}
		#endregion

		#region properties
		public CommandType CommandType
		{
			get { return m_DbCommand.CommandType; }
			set { m_DbCommand.CommandType = value; }
		}

		public string CommandText
		{
			get { return m_DbCommand.CommandText; }
			set { m_DbCommand.CommandText = value; }
		}
		
		public int CommandTimeout
		{
			get { return m_DbCommand.CommandTimeout; }
			set { m_DbCommand.CommandTimeout = value; }
		}

		public NeweggDatabase Database
		{
			get { return (NeweggDatabase)Enum.Parse(typeof(NeweggDatabase), m_DatabaseName); }
			set { m_DatabaseName = value.ToString(); }
		}
		#endregion

		#region add parameter
		/// <summary>
		/// Adds the input parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="dataType">Type of the db.</param>
		/// <param name="value">The value.</param>
		public void AddInputParameter(string name, DbType dataType, object value)
		{
			ActualDatabase.AddInParameter(m_DbCommand, name, dataType, value);
		}

		/// <summary>
		/// Adds the input parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="dataType">Type of the db.</param>
		public void AddInputParameter(string name, DbType dataType)
		{
			ActualDatabase.AddInParameter(m_DbCommand, name, dataType);
		}

		/// <summary>
		/// Adds the input parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="dataType">Type of the db.</param>
		/// <param name="size">The size.</param>
		/// <param name="value">The value.</param>
		public void AddInputParameter(string name, DbType dataType, int size, object value)
		{
			//ActualDatabase.AddParameter(m_DbCommand, name, dataType, size, ParameterDirection.Input, false, 0, 0, String.Empty, DataRowVersion.Default, value);
			AddInputParameterWithSize(name, dataType, size, value);
		}

		public void AddOutputParameter(string name, DbType dataType, int size)
		{
			ActualDatabase.AddOutParameter(m_DbCommand, name, dataType, size);
		}
		#endregion

		#region add parameter group
		public void AddInputParameterGroup(string name, DbType type, string parameters)
		{
			AddInputParameterGroup(name, type, -1, parameters);
		}

		public void AddInputParameterGroup(string name, DbType type, ICollection parameters)
		{
			AddInputParameterGroup(name, type, -1, parameters);
		}

		public void AddInputParameterGroup(string name, DbType type, int size, string parameters)
		{
			AssertUtils.ArgumentHasText(parameters, parameters);
			AddInputParameterGroup(name, type, size, parameters.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
		}

		public void AddInputParameterGroup(string name, DbType type, int size, ICollection parameters)
		{
			AddInputParametersWithSize(name, type, size, parameters);
		}
		#endregion
	}
}
