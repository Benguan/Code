using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.Framework.Utility
{
	/// <summary>
	/// trace performance by key
	/// </summary>
	public class PerformanceTracer
	{
		/// <summary>
		/// Structure
		/// </summary>
		public PerformanceTracer()
		{

		}

		/// <summary>
		/// Structure
		/// </summary>
		/// <param name="minMilliseconds">Only time-consuming than minMilliseconds event will be recorded</param>
		public PerformanceTracer(int minMilliseconds)
		{
			this.m_minMilliseconds = minMilliseconds;
		}

		/// <summary>
		/// Only time-consuming than minMilliseconds event will be recorded
		/// </summary>
		private readonly int m_minMilliseconds;

		/// <summary>
		/// Trace message builder
		/// </summary>
		private StringBuilder m_TraceMessageBuilder = new StringBuilder();

		/// <summary>
		/// Start time dictionary
		/// </summary>
		private Dictionary<string, DateTime> m_Start = new Dictionary<string, DateTime>();

		/// <summary>
		/// trace level
		/// </summary>
		private int m_level = default(int);

		/// <summary>
		/// Add trace
		/// </summary>
		/// <param name="key">trace key</param>
		public void TraceBegin(string key)
		{
			if (!string.IsNullOrEmpty(key))
			{
				string formatKey = key.Trim().ToUpper();
				if (!this.m_Start.ContainsKey(formatKey))
				{
					this.m_Start.Add(formatKey, DateTime.Now);
					m_level++;
				}
			}
		}

		/// <summary>
		/// close trace and record message
		/// </summary>
		/// <param name="key"></param>
		public void TraceEnd(string key)
		{
			this.TraceEnd(key, 0);
		}

		/// <summary>
		/// close trace and record message
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="minMilliseconds">Only time-consuming than minMilliseconds event will be recorded</param>
		public void TraceEnd(string key, int minMilliseconds)
		{
			if (!string.IsNullOrEmpty(key))
			{
				string formatKey = key.Trim().ToUpper();

				DateTime endTime = DateTime.Now;
				if (this.m_Start.ContainsKey(formatKey))
				{
					DateTime startTime = this.m_Start[formatKey];
					TimeSpan duration = endTime - startTime;

					if (duration.TotalMilliseconds >= minMilliseconds && duration.TotalMilliseconds >= m_minMilliseconds)
					{
						for (int index = 0; index < m_level - 1; index++)
						{
							m_TraceMessageBuilder.Append("\t");
						}
						m_TraceMessageBuilder.Append(key);
						m_TraceMessageBuilder.Append(": [");
						m_TraceMessageBuilder.Append(duration.TotalMilliseconds);
						m_TraceMessageBuilder.Append("] [");
						m_TraceMessageBuilder.Append(startTime.TimeOfDay.ToString());
						m_TraceMessageBuilder.Append(" - ");
						m_TraceMessageBuilder.Append(endTime.TimeOfDay.ToString());
						m_TraceMessageBuilder.Append("]\n");
					}

					this.m_Start.Remove(formatKey);

					m_level--;
				}
			}
		}

		/// <summary>
		/// Trace message
		/// </summary>
		public string TraceMessage
		{
			get { return m_TraceMessageBuilder.ToString(); }
		}
	}
}
