/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Dolphin Zhang (DolphinZhang@newegg.com)
 * Create Date:  04/10/2007 14:01:19
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace Newegg.Framework.Logging
{
	public static class LoggerFactory
	{
        private const string LOGGER_TYPE_NAME = "LoggerTypeName";
		private static ILogger m_SingleLogger = null;
		private static object m_SynObj = new object();
		public static ILogger CreateLogger()
		{
                if (m_SingleLogger == null)
                {
                    lock (m_SynObj)
                    {
                        if (m_SingleLogger == null)
                        {
                            try
                            {
                                string[] loggerTypeName = ConfigurationManager.AppSettings[LOGGER_TYPE_NAME].Split(',');
                                string fullLoggerClassName = loggerTypeName[0];
                                string loggerAssembleName = loggerTypeName[1];
                                m_SingleLogger = (ILogger)Assembly.Load(loggerAssembleName).CreateInstance(fullLoggerClassName);
                                if (m_SingleLogger == null)
                                {
                                    m_SingleLogger = new EmptyLogger();
                                }
                            }
                            catch
                            {
                                m_SingleLogger = new EmptyLogger();
                            }
                        }
                    }
                }
                return m_SingleLogger;
		}
	}
}
