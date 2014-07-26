/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  08/19/2006
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

using Newegg.Framework.Logging;
using Newegg.Framework.Collection;

namespace Newegg.Framework.Utility
{
	public class FileSystemChangeEventHandler
	{
		#region logging
		private const string s_LogCategory = "Framework.FileSystemChangeEventHandler";
		private const int LogEventReceiveNotification = 1;
		private const int LogEventDispatchNotification = 2;

		[Conditional("TRACE")]
		private static void LogFileChange(int timeout, string fileName, 
			WatcherChangeTypes changeType, FileSystemEventHandler handler)
		{
            LoggerFactory.CreateLogger().LogEvent(s_LogCategory, LogEventReceiveNotification, timeout.ToString(), changeType.ToString(), fileName, handler.ToString());
		}

        [Conditional("TRACE")]
        private static void LogActualHandleFileChange(FileChangeEventArg arg)
        {
            LoggerFactory.CreateLogger().LogEvent(
                s_LogCategory,
                LogEventDispatchNotification,
                arg.Argument.ChangeType.ToString(),
                arg.Argument.FullPath);
        }
		#endregion

		private class FileChangeEventArg
		{
			private object m_Sender;
			private FileSystemEventArgs m_Argument;

			public FileChangeEventArg(object sender, FileSystemEventArgs arg)
			{
				m_Sender = sender;
				m_Argument = arg;
			}

			public object Sender
			{
				get { return m_Sender; }
			}
			public FileSystemEventArgs Argument
			{
				get { return m_Argument; }
			}
		}

		private object m_SyncObject;

		private Dictionary<string, Timer> m_Timers;
		private int m_Timeout;
		private bool m_IsFolderChange;

		public event FileSystemEventHandler ActualHandler;

		private FileSystemChangeEventHandler()
		{
			m_SyncObject = new object();
			m_Timers = new Dictionary<string, Timer>(new CaseInsensitiveStringEqualityComparer());
		}

		public FileSystemChangeEventHandler(int timeout)
			: this(timeout, false)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemChangeEventHandler"/> class.
		/// </summary>
		/// <param name="timeout">The timeout.</param>
		/// <param name="isFolderChange">if set to <c>true</c> [if the Watcher is to folder change].</param>
		public FileSystemChangeEventHandler(int timeout, bool isFolderChange)
			: this()
		{
			m_Timeout = timeout;
			m_IsFolderChange = isFolderChange;
		}

		public void ChangeEventHandler(object sender, FileSystemEventArgs e)
		{
			lock (m_SyncObject)
			{
				//LogFileChange(m_Timeout, e.FullPath, e.ChangeType, ActualHandler);
				Timer t;

				string watchPath;

				if (m_IsFolderChange)
				{
					watchPath = Path.GetDirectoryName(e.FullPath);
				}
				else
				{
					watchPath = e.FullPath;
				}

				// disable the existing timer
				if (m_Timers.ContainsKey(watchPath))
				{
					t = m_Timers[watchPath];
					t.Change(Timeout.Infinite, Timeout.Infinite);
					t.Dispose();
				}

				// add a new timer
				if (ActualHandler != null)
				{
					t = new Timer(TimerCallback, new FileChangeEventArg(sender, e), m_Timeout, Timeout.Infinite);
					m_Timers[watchPath] = t;
				}
			}
		}

		private void TimerCallback(object state)
		{
			FileChangeEventArg arg = state as FileChangeEventArg;
			//LogActualHandleFileChange(arg);
			ActualHandler(arg.Sender, arg.Argument);
		}
	}
}
