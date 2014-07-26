/*****************************************************************
 * Copyright (C) 2005-2006 Newegg Corporation
 * All rights reserved.
 * 
 * Author:   Jason Huang (jaosn.j.huang@newegg.com)
 * Create Date:  07/09/2008 15:12:41
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Globalization;

namespace Newegg.Framework.Utility
{
	/// <summary>
	/// Assertion utility methods that simplify things such as argument checks.
	/// </summary>
	/// <remarks>
	/// <p>
	/// Not intended to be used directly by applications.
	/// </p>
	/// </remarks>
	/// <author>Aleksandar Seovic</author>
	/// <version>$Id: AssertUtils.cs,v 1.13 2008/03/14 10:45:08 bbaia Exp $</version>
	public sealed class AssertUtils
	{
		/// <summary>
		/// Checks the value of the supplied <paramref name="argument"/> and throws an
		/// <see cref="System.ArgumentNullException"/> if it is <see langword="null"/>.
		/// </summary>
		/// <param name="argument">The object to check.</param>
		/// <param name="name">The argument name.</param>
		/// <exception cref="System.ArgumentNullException">
		/// If the supplied <paramref name="argument"/> is <see langword="null"/>.
		/// </exception>
		public static void ArgumentNotNull(object argument, string name)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name,
					string.Format(CultureInfo.InvariantCulture, "Argument '{0}' cannot be null.", name));
			}
		}

		/// <summary>
		/// Checks the value of the supplied <paramref name="argument"/> and throws an
		/// <see cref="System.ArgumentNullException"/> if it is <see langword="null"/>.
		/// </summary>
		/// <param name="argument">The object to check.</param>
		/// <param name="name">The argument name.</param>
		/// <param name="message">
		/// An arbitrary message that will be passed to any thrown
		/// <see cref="System.ArgumentNullException"/>.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the supplied <paramref name="argument"/> is <see langword="null"/>.
		/// </exception>
		public static void ArgumentNotNull(object argument, string name, string message)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name, message);
			}
		}

		/// <summary>
		/// Checks the value of the supplied string <paramref name="argument"/> and throws an
		/// <see cref="System.ArgumentNullException"/> if it is <see langword="null"/> or
		/// contains only whitespace character(s).
		/// </summary>
		/// <param name="argument">The string to check.</param>
		/// <param name="name">The argument name.</param>
		/// <exception cref="System.ArgumentNullException">
		/// If the supplied <paramref name="argument"/> is <see langword="null"/> or
		/// contains only whitespace character(s).
		/// </exception>
		public static void ArgumentHasText(string argument, string name)
		{
			if (StringHelper.IsNullOrEmpty(argument))
			{
				throw new ArgumentNullException(name,
					string.Format(CultureInfo.InvariantCulture, "Argument '{0}' cannot be null or resolve to an empty string : '{1}'.", name, argument));
			}
		}

		/// <summary>
		/// Checks the value of the supplied string <paramref name="argument"/> and throws an
		/// <see cref="System.ArgumentNullException"/> if it is <see langword="null"/> or
		/// contains only whitespace character(s).
		/// </summary>
		/// <param name="argument">The string to check.</param>
		/// <param name="name">The argument name.</param>
		/// <param name="message">
		/// An arbitrary message that will be passed to any thrown
		/// <see cref="System.ArgumentNullException"/>.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// If the supplied <paramref name="argument"/> is <see langword="null"/> or
		/// contains only whitespace character(s).
		/// </exception>
		public static void ArgumentHasText(string argument, string name, string message)
		{
			if (StringHelper.IsNullOrEmpty(argument))
			{
				throw new ArgumentNullException(name, message);
			}
		}

		/// <summary>
		/// Checks the value of the supplied <see cref="ICollection"/> <paramref name="argument"/> and throws
		/// an <see cref="ArgumentNullException"/> if it is <see langword="null"/> or contains no elements.
		/// </summary>
		/// <param name="argument">The array or collection to check.</param>
		/// <param name="name">The argument name.</param>
		/// <exception cref="System.ArgumentNullException">
		/// If the supplied <paramref name="argument"/> is <see langword="null"/> or
		/// contains no elements.
		/// </exception>
		public static void ArgumentHasLength(ICollection argument, string name)
		{
			if (!ArrayUtils.HasLength(argument))
			{
				throw new ArgumentNullException(name,
					string.Format(CultureInfo.InvariantCulture, "Argument '{0}' cannot be null or resolve to an empty array", name));
			}
		}

		/// <summary>
		/// Checks the value of the supplied <see cref="ICollection"/> <paramref name="argument"/> and throws
		/// an <see cref="ArgumentNullException"/> if it is <see langword="null"/> or contains no elements.
		/// </summary>
		/// <param name="argument">The array or collection to check.</param>
		/// <param name="name">The argument name.</param>
		/// <param name="message">An arbitrary message that will be passed to any thrown <see cref="System.ArgumentNullException"/>.</param>
		/// <exception cref="System.ArgumentNullException">
		/// If the supplied <paramref name="argument"/> is <see langword="null"/> or
		/// contains no elements.
		/// </exception>
		public static void ArgumentHasLength(ICollection argument, string name, string message)
		{
			if (!ArrayUtils.HasLength(argument))
			{
				throw new ArgumentNullException(name, message);
			}
		}

		/// <summary>
		/// Checks whether the specified <paramref name="argument"/> can be cast 
		/// into the <paramref name="requiredType"/>.
		/// </summary>
		/// <param name="argument">
		/// The argument to check.
		/// </param>
		/// <param name="argumentName">
		/// The name of the argument to check.
		/// </param>
		/// <param name="requiredType">
		/// The required type for the argument.
		/// </param>
		/// <param name="message">
		/// An arbitrary message that will be passed to any thrown
		/// <see cref="System.ArgumentException"/>.
		/// </param>
		public static void AssertArgumentType(object argument, string argumentName, Type requiredType, string message)
		{
			if (argument != null && requiredType != null && !requiredType.IsAssignableFrom(argument.GetType()))
			{
				throw new ArgumentException(message, argumentName);
			}
		}
	}
}
