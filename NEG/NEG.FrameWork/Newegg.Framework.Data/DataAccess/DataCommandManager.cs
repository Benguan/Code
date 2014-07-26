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
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

using Newegg.Framework.Threading;
using Newegg.Framework.Collection;
using Newegg.Framework.Utility;
using Newegg.Framework.Logging;
using Newegg.Framework.Globalization;
using Newegg.Framework.DataAccess.Configuration;

namespace Newegg.Framework.DataAccess
{
	//using DataCommandHashtable = Dictionary<string, DataCommand>;
	using DataCommandHashtable = Dictionary<string, Dictionary<string, DataCommand>>;
    using System.Text.RegularExpressions;
	
	public static class DataCommandManager
	{
		#region fields
		private const string EventCategory = "DataCommandManager";
		private const int FILE_CHANGE_NOTIFICATION_INTERVAL = 500;
		
		private static FileSystemChangeEventHandler s_FileChangeHandler;
		private static object s_CommandSyncObject;
		private static object s_CommandFileListSyncObject;

		private static FileSystemWatcher s_Watcher;
		private static DataCommandHashtable s_DataCommands;
		private static string s_DataFileFolder;

		/// <summary>
		/// records datacommand file and command list relationship
		/// key: file name
		/// value: list of datacommand names
		/// </summary>
		private static Dictionary<string, IList<string>> s_FileCommands;

		private static readonly string DefaultLanguageCode = "en";
		private static readonly string MultiLangFileNamePlaceHolder = ".MultiLang.";
		#endregion

		static DataCommandManager()
		{
			s_FileChangeHandler = new FileSystemChangeEventHandler(FILE_CHANGE_NOTIFICATION_INTERVAL);
			s_FileChangeHandler.ActualHandler += new FileSystemEventHandler(Watcher_Changed);

			s_DataFileFolder = Path.GetDirectoryName(DataAccessSetting.DataCommandFileListConfigFile);

			s_CommandSyncObject = new object();
			s_CommandFileListSyncObject = new object();

			s_Watcher = new FileSystemWatcher(s_DataFileFolder);
			s_Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime;
			s_Watcher.Changed += new FileSystemEventHandler(s_FileChangeHandler.ChangeEventHandler);
			s_Watcher.EnableRaisingEvents = true;

			UpdateAllCommandFiles();
		}

		/// <summary>
		/// invoked when a file change occurs.
		/// Note:
		///		1. one change at a time.
		///		2. if the inventory file changes then all the datacommands are reloaded.
		///		3. this function is thread safe.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			// check datacommand list file
			if (string.Compare(e.FullPath, DataAccessSetting.DataCommandFileListConfigFile, true) == 0)
			{
				// reload all datacommands
				UpdateAllCommandFiles();
				return;
			}

			// check data command file
			lock (s_CommandFileListSyncObject)
			{
				foreach (string file in s_FileCommands.Keys)
				{
					if (string.Compare(file, e.FullPath, true) == 0)
					{
						UpdateCommandFile(file);
						// only one file is watched at a time. 
						// if break is not used here, s_FileCommands is changed in UpdateCommandFile and an exception
						// will be thrown in the next iteration.
						break;
					}
				}
			}
		}

		private static string DataCommandListFileName
		{
			get { return Path.GetFileName(DataAccessSetting.DataCommandFileListConfigFile); }
		}

		private static Dictionary<string, DataCommand> GetDataCommandHashtableByLanguageCode(string languageCode)
		{
			if (s_DataCommands.ContainsKey(languageCode))
			{
				return s_DataCommands[languageCode];
			}
			return (new Dictionary<string, DataCommand>());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m_FileNamePattern"></param>
		/// <exception cref="DataCommandFileLoadException">m_FileNamePattern does not exist or contains invalid information</exception>
		private static void UpdateCommandFile(string fileName)
		{
			IList<string> commandNames;
			bool isMultiLang = (fileName.IndexOf(MultiLangFileNamePlaceHolder, StringComparison.InvariantCultureIgnoreCase) != -1);

			if (s_FileCommands.ContainsKey(fileName))
			{
				commandNames = s_FileCommands[fileName];
			}
			else
			{
				commandNames = null;
			}

			lock (s_CommandSyncObject)
			{
				// load from file and add to commands
				DataOperationConfiguration commands = ObjectXmlSerializer.LoadFromXml<DataOperationConfiguration>(fileName);
				if (commands == null)
				{
					throw new DataCommandFileLoadException(fileName);
				}

				if (!isMultiLang)
				{
					UpdateDefaultCommandFile(commands, commandNames);
				}
				else
				{
					UpdateMultiLangCommandFile(commands, commandNames);
				}

				// update file-command list relationship
				s_FileCommands[fileName] = commands.GetCommandNames();
				//DataAccessLogger.LogDatabaseCommandFileLoaded(fileName);
			}
		}

		private static void UpdateMultiLangCommandFile(DataOperationConfiguration commands, IList<string> commandNames)
		{
			List<string> languageCodeList = BizUnitSetting.BizUnitConfig.GetAvaliableLanguageCode(DefaultLanguageCode);
			if (languageCodeList != null && languageCodeList.Count > 0 && commands.DataCommandList != null && commands.DataCommandList.Length > 0)
			{
				Dictionary<string, DataCommand> newCommands;
				foreach (string languageCode in languageCodeList)
				{
					newCommands = new Dictionary<string, DataCommand>(GetDataCommandHashtableByLanguageCode(languageCode));
					if (commandNames != null)
					{
						foreach (string commandName in commandNames)
						{
							newCommands.Remove(commandName);
						}
					}

					foreach (DataOperationCommand cmd in commands.DataCommandList)
					{
						if (newCommands.ContainsKey(cmd.Name))
						{
							throw new Exception(string.Format("The DataCommand configuration file has a same key {0} now.", cmd.Name));
						}

						DataCommand command = cmd.GetDataCommand();
                        command.DbCommand.CommandText = command.DbCommand.CommandText.Replace(StringResource.ThreadStorage_Value_MultiLang_PlaceHolder, languageCode);

						newCommands.Add(cmd.Name, command);
					}

					s_DataCommands[languageCode] = newCommands;
				}
			}
		}

		private static void UpdateDefaultCommandFile(DataOperationConfiguration commands,IList<string> commandNames)
		{
			// copy from existing hashtable
			Dictionary<string, DataCommand> newCommands = new Dictionary<string, DataCommand>(GetDataCommandHashtableByLanguageCode(DefaultLanguageCode));

			// remove existing data commands
			if (commandNames != null)
			{
				foreach (string commandName in commandNames)
				{
					newCommands.Remove(commandName);
				}
			}

			if (commands.DataCommandList != null && commands.DataCommandList.Length > 0)
			{
				foreach (DataOperationCommand cmd in commands.DataCommandList)
				{
					if (newCommands.ContainsKey(cmd.Name))
					{
						throw new Exception(string.Format("The DataCommand configuration file has a same key {0} now.", cmd.Name));
					}
                    newCommands.Add(cmd.Name, cmd.GetDataCommand());
				}

				s_DataCommands[DefaultLanguageCode] = newCommands;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="DataCommandFileNotSpecifiedException"> if the datacommand file list 
		/// configuration file does not contain any valid file name.
		/// </exception>
		private static void UpdateAllCommandFiles()
		{
			lock (s_CommandFileListSyncObject)
			{
				// reload file list content
				ConfigDataCommandFileList fileList =
					ObjectXmlSerializer.LoadFromXml<ConfigDataCommandFileList>(DataAccessSetting.DataCommandFileListConfigFile);
				if (fileList == null || fileList.FileList == null || fileList.FileList.Length == 0)
				{
					throw new DataCommandFileNotSpecifiedException();
				}

				//DataAccessLogger.LogDataCommandInventoryFileLoaded(DataAccessSetting.DataCommandFileListConfigFile, 
				//    fileList.FileList.Length);

				// clear file-command name relationship
				s_FileCommands = new Dictionary<string, IList<string>>();

				// clear commands
				s_DataCommands = new DataCommandHashtable(new CaseInsensitiveStringEqualityComparer());

				// update each datacommand file
				foreach (ConfigDataCommandFileList.DataCommandFile commandFile in fileList.FileList)
				{
					string fileName = Path.Combine(s_DataFileFolder, commandFile.FileName);
					UpdateCommandFile(fileName);
				}
			}
		}

		private static DataCommand GetDataCommandInternal(string name)
		{
			if (s_DataCommands[ResourceProfile.CurrentLanguageCode].ContainsKey(name))
			{
				return s_DataCommands[ResourceProfile.CurrentLanguageCode][name].Clone() as DataCommand;
			}
			return s_DataCommands[DefaultLanguageCode][name].Clone() as DataCommand;
		}

		/// <summary>
		/// Get DataCommand corresponding to the given command name.
		/// </summary>
		/// <param name="name">Name of the DataCommand </param>
		/// <returns>DataCommand</returns>
		/// <exception cref="KeyNotFoundException">the specified DataCommand does not exist.</exception>
		public static DataCommand GetDataCommand(string name)
		{
			// Logger.LogSystemInfo(EventCategory, "Retrieving datacommand: " + name);
			return GetDataCommandInternal(name);
		}

		public static DataCommand GetDataCommand(string name, string databaseName)
		{
			// Logger.LogSystemInfo(EventCategory, "Retrieving datacommand: " + name);
			DataCommand cmd = GetDataCommandInternal(name);
			cmd.DatabaseName = databaseName;
			return cmd;
		}

		/// <summary>
		/// Get DataCommand corresponding to the given command name.
		/// </summary>
		/// <param name="name">Name of the DataCommand </param>
		/// <returns>DataCommand</returns>
		/// <exception cref="KeyNotFoundException">the specified DataCommand does not exist.</exception>
		public static DataCommand GetDataCommand(string name, bool supportTran)
		{
			// Logger.LogSystemInfo(EventCategory, "Retrieving datacommand: " + name);
			DataCommand command = GetDataCommandInternal(name);
			command.SupportTransaction = supportTran;
			return command;
		}

		/// <summary>
		/// Refreshes the data command while remaining the command's execution context (db connection, transaction, etc).
		/// </summary>
		/// <param name="cmd">The CMD.</param>
		/// <param name="name">The name.</param>
		public static void RefreshDataCommand(DataCommand command, string name)
		{
			DataCommand cmd = GetDataCommand(name);
			command.CopyCommand(cmd);
		}

		/// <summary>
		/// Refreshes the data command while remaining the command's execution context (db connection, transaction, etc).
		/// </summary>
		/// <param name="command">The command.</param>
		/// <param name="fromCommand">From command.</param>
		public static void RefreshDataCommand(DataCommand command, DataCommand fromCommand)
		{
			command.CopyCommand(fromCommand);
		}

		//not advice use
		public static CustomDataCommand CreateCustomDataCommand(NeweggDatabase database)
		{
			return new CustomDataCommand(database.ToString());
		}
		public static CustomDataCommand CreateCustomDataCommand(string database)
		{
			return new CustomDataCommand(database);
		}

		//not advice use
		public static CustomDataCommand CreateCustomDataCommand(NeweggDatabase database, bool supportTran)
		{
			return new CustomDataCommand(database.ToString(), supportTran);
		}
		public static CustomDataCommand CreateCustomDataCommand(string database, bool supportTran)
		{
			return new CustomDataCommand(database, supportTran);
		}

		//not advice use
		public static CustomDataCommand CreateCustomDataCommand(NeweggDatabase database, CommandType commandType)
		{
			return new CustomDataCommand(database.ToString(), commandType);
		}
		public static CustomDataCommand CreateCustomDataCommand(string database, CommandType commandType)
		{
			return new CustomDataCommand(database, commandType);
		}

		//not advice use
		public static CustomDataCommand CreateCustomDataCommand(NeweggDatabase database, CommandType commandType, bool supportTran)
		{
			return new CustomDataCommand(database.ToString(), commandType, supportTran);
		}
		public static CustomDataCommand CreateCustomDataCommand(string database, CommandType commandType, bool supportTran)
		{
			return new CustomDataCommand(database, commandType, supportTran);
		}

		//not advice use
		public static CustomDataCommand CreateCustomDataCommand(NeweggDatabase database, CommandType commandType, string commandText)
		{
			return new CustomDataCommand(database.ToString(), commandType, commandText);
		}
		public static CustomDataCommand CreateCustomDataCommand(string database, CommandType commandType, string commandText)
		{
			return new CustomDataCommand(database, commandType, commandText);
		}

		//not advice use
		public static CustomDataCommand CreateCustomDataCommand(NeweggDatabase database, CommandType commandType, string commandText, bool supportTran)
		{
			return new CustomDataCommand(database.ToString(), commandType, commandText, supportTran);
		}
		public static CustomDataCommand CreateCustomDataCommand(string database, CommandType commandType, string commandText, bool supportTran)
		{
			return new CustomDataCommand(database, commandType, commandText, supportTran);
		}

       
	}
}
