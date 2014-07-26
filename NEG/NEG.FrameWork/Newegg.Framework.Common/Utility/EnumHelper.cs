using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Newegg.Framework.Utility
{
	/// <summary>
	//EnumUtil
	/// </summary>
	public static class EnumHelper
	{
		/// <summary>
		/// get the enum's all list
		/// </summary>
		/// <param name="enumType">枚举的类型</param>
		/// <returns></returns>
		public static List<EnumItem> GetEnumItems(Type enumType)
		{
			return GetEnumItems(enumType, false);
		}

		/// <summary>
		/// 获得枚举类型所包含的全部项的列表，包含"All"。
		/// </summary>
		/// <param name="enumType">the type of the enum</param>
		/// <returns></returns>
		public static List<EnumItem> GetEnumItemsWithAll(Type enumType)
		{
			return GetEnumItems(enumType, true);
		}

		/// <summary>
		/// get the enum's all list
		/// </summary>
		/// <param name="enumType">the type of the enum</param>
		/// <param name="withAll">identicate whether the returned list should contain the all item</param>
		/// <returns></returns>
		public static List<EnumItem> GetEnumItems(Type enumType, bool withAll)
		{
			List<EnumItem> list = new List<EnumItem>();

			if (enumType.IsEnum != true)
			{
				// just whethe the type is enum type
				throw new InvalidOperationException();
			}

			if (withAll == true)
			{
				list.Add(new EnumItem(-1, "All"));
			}

			// 获得特性Description的类型信息
			Type typeDescription = typeof(DescriptionAttribute);

			// 获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
			System.Reflection.FieldInfo[] fields = enumType.GetFields();

			// 检索所有字段
			foreach (FieldInfo field in fields)
			{
				// 过滤掉一个不是枚举值的，记录的是枚举的源类型
				if (field.FieldType.IsEnum == false)
				{
					continue;
				}

				// 通过字段的名字得到枚举的值
				int value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
				string text = string.Empty;

				// 获得这个字段的所有自定义特性，这里只查找Description特性
				object[] arr = field.GetCustomAttributes(typeDescription, true);
				if (arr.Length > 0)
				{
					// 因为Description自定义特性不允许重复，所以只取第一个
					DescriptionAttribute aa = (DescriptionAttribute)arr[0];

					// 获得特性的描述值
					text = aa.Description;
				}
				else
				{
					// 如果没有特性描述，那么就显示英文的字段名
					text = field.Name;
				}
				list.Add(new EnumItem(value, text));
			}

			return list;
		}

		/// <summary>
		/// the the enum value's descrption attribute information
		/// </summary>
		/// <param name="enumType">the type of the enum</param>
		/// <param name="value">the enum value</param>
		/// <returns></returns>
		public static string GetEnumValueDescription<T>(T t)
		{
			Type enumType = typeof(T);
			List<EnumItem> list = GetEnumItems(enumType);
			foreach (EnumItem item in list)
			{
				if (Convert.ToInt32(item.Key) == Convert.ToInt32(t))
				{
					return item.Value.ToString();
				}
			}
			return string.Empty;
		}

		/// <summary>
		/// get the enum value's int mode value
		/// </summary>
		/// <param name="enumType">the type of the enum</param>
		/// <param name="value">the enum value's descrption</param>
		/// <returns></returns>
		public static int GetEnumValueByDescription<T>(string description)
		{
			Type enumType = typeof(T);
			List<EnumItem> list = GetEnumItems(enumType);
			foreach (EnumItem item in list)
			{
				if (item.Value.ToString().ToLower() == description.Trim().ToLower())
				{
					return Convert.ToInt32(item.Key);
				}
			}
			return -1;
		}
		
		/// <summary>
		/// get the Enum value according to the its decription
		/// </summary>
		/// <param name="enumType">the type of the enum</param>
		/// <param name="value">the description of the EnumValue</param>
		/// <returns></returns>
		public static T GetEnumByDescription<T>(string description)
		{
			Type enumType = typeof(T);
			List<EnumItem> list = GetEnumItems(enumType);
			foreach (EnumItem item in list)
			{
				if (item.Value.ToString().ToLower() == description.Trim().ToLower())
				{
					return (T)item.Key;
				}
			}
			return default(T);
		}
		
		/// <summary>
		/// get the description attribute of a Enum value
		/// </summary>
		/// <param name="enumType">the type of the enum</param>
		/// <param name="value">enum value name</param>
		/// <returns></returns>
		public static T GetEnumByName<T>(string name)
		{
			Type enumType = typeof(T);
			List<EnumItem> list = GetEnumItems(enumType);
			foreach (EnumItem item in list)
			{
				if (item.Value.ToString().ToLower() == name.Trim().ToLower())
				{
					return (T)item.Key;
				}
			}
			return default(T);
		}

		public static T GetEnumByValue<T>(object key)
		{
			if (key == null)
			{
				return default(T);
			}
			try
			{
				Type enumType = typeof(T);
				List<EnumItem> list = GetEnumItems(enumType);
				foreach (EnumItem item in list)
				{
					if (item.Key.ToString().Trim().ToLower() == key.ToString().Trim().ToLower())
					{
						return (T)item.Key;
					}
				}
				return default(T);
			}
			catch
			{
				return default(T);
			}
		}
	}

	public class EnumItem
	{
		private object m_key;
		private object m_value;

		public object Key
		{
			get { return m_key; }
			set { m_key = value; }
		}

		public object Value
		{
			get { return m_value; }
			set { m_value = value; }
		}

		public EnumItem(object key, object value)
		{
			m_key = key;
			m_value = value;
		}
	}
}
