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
		/// <param name="enumType">ö�ٵ�����</param>
		/// <returns></returns>
		public static List<EnumItem> GetEnumItems(Type enumType)
		{
			return GetEnumItems(enumType, false);
		}

		/// <summary>
		/// ���ö��������������ȫ������б�����"All"��
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

			// �������Description��������Ϣ
			Type typeDescription = typeof(DescriptionAttribute);

			// ���ö�ٵ��ֶ���Ϣ����Ϊö�ٵ�ֵʵ������һ��static���ֶε�ֵ��
			System.Reflection.FieldInfo[] fields = enumType.GetFields();

			// ���������ֶ�
			foreach (FieldInfo field in fields)
			{
				// ���˵�һ������ö��ֵ�ģ���¼����ö�ٵ�Դ����
				if (field.FieldType.IsEnum == false)
				{
					continue;
				}

				// ͨ���ֶε����ֵõ�ö�ٵ�ֵ
				int value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);
				string text = string.Empty;

				// �������ֶε������Զ������ԣ�����ֻ����Description����
				object[] arr = field.GetCustomAttributes(typeDescription, true);
				if (arr.Length > 0)
				{
					// ��ΪDescription�Զ������Բ������ظ�������ֻȡ��һ��
					DescriptionAttribute aa = (DescriptionAttribute)arr[0];

					// ������Ե�����ֵ
					text = aa.Description;
				}
				else
				{
					// ���û��������������ô����ʾӢ�ĵ��ֶ���
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
