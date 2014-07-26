/*****************************************************************
 * Copyright (C) Newegg Corporation. All rights reserved.
 * 
 * Author:   Cheney Hu (cheney.c.hu@newegg.com)
 * Create Date:  02/26/2009 10:08:54
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/

using System.Collections.Generic;

namespace Newegg.Framework.Globalization
{
	internal class ResourcePool
	{
		private static ResourcePoolImpl s_Instance = new ResourcePoolImpl();

		public static ResourceLoader GetLoader(string baseName, string resourceDir)
		{
			return s_Instance.GetLoader(baseName, resourceDir);
		}

		public static void Release(ResourceLoader loader)
		{
			s_Instance.Release(loader);
		}

		private class PooledItem
		{
			public ResourceLoader Loader;
			public long Referenced;
		}

		private class ResourcePoolImpl
		{
			private Dictionary<string, PooledItem> m_PooledItemMap = new Dictionary<string, PooledItem>();

			public ResourceLoader GetLoader(string baseName, string resourceDir)
			{
				PooledItem item = null;

				string key = MakeKey(baseName, resourceDir);

                if (!m_PooledItemMap.TryGetValue(key, out item))
                {
                    lock (m_PooledItemMap)
                    {
                        if (!m_PooledItemMap.TryGetValue(key, out item))
                        {
                            item = new PooledItem();
                            item.Loader = new ResourceLoader(baseName, resourceDir);
                            item.Referenced = 1;
                            m_PooledItemMap[key] = item;
                        }
                        else
                        {
                            item.Referenced++;
                        }
                    }
                }

				return item.Loader;
			}

			private string MakeKey(string baseName, string resourceDir)
			{
				return (baseName + "@" + resourceDir).ToUpper();
			}

			public void Release(ResourceLoader loader)
			{
				lock (m_PooledItemMap)
				{
					string key = null;
					foreach (KeyValuePair<string, PooledItem> pair in m_PooledItemMap)
					{
						PooledItem item = pair.Value;
						if (loader == item.Loader)
						{
							key = pair.Key;
							item.Referenced--;
							if (item.Referenced > 0)
							{
								return;
							}
							break;
						}
					}
					if (!string.IsNullOrEmpty(key))
					{
						m_PooledItemMap.Remove(key);
						loader.Dispose();
					}
				}
			}
		}
	}
}
