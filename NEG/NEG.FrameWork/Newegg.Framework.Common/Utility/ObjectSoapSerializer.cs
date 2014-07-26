/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Robert Wang (robert.q.wang@newegg.com)
 * Create Date:  06/16/2007 09:58:39
 * Usage:
 *
*****************************************************************/

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;

namespace Newegg.Framework.Utility
{
	/// <summary>
	/// serializer using SoapFormatter
	/// </summary>
	public static class ObjectSoapSerializer
	{
		/// <summary>
		/// Serializes the specified o.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns></returns>
		public static string Serialize(object o)
		{
			MemoryStream ms = new MemoryStream();
			SoapFormatter sf = new SoapFormatter();
			sf.Serialize(ms, o);
			return Encoding.UTF8.GetString(ms.ToArray());
		}

		/// <summary>
		/// Deserializes the specified MSG.
		/// returns null if failed.
		/// </summary>
		/// <param name="msg">The MSG.</param>
		/// <returns></returns>
		public static T Deserialize<T>(string msg) where T : class
		{
			byte[] buffer = Encoding.UTF8.GetBytes(msg);
			MemoryStream ms = new MemoryStream(buffer);
			SoapFormatter sf = new SoapFormatter();
			return sf.Deserialize(ms) as T;
		}
	}
}
