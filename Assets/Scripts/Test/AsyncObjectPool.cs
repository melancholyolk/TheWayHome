using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Fundamental{
	public struct InstantiateGO : IEnumerator
	{
		public GameObject result;
		public PrecacheItem precache;
		public delegate void CallBack(InstantiateGO go);

		public event CallBack Complete
		{
			add
			{
				if (result != null)
				{
					value?.Invoke(this);
					return;
				}
				if (precache.load.IsDone)
				{
					result = GameObject.Instantiate(precache.load.Result);
					value?.Invoke(this);
				}
				else
				{
					InstantiateGO go = this;
					var pre = precache;
					precache.load.Completed += (l) =>
					{
						if (l.Status == AsyncOperationStatus.Succeeded)
						{
							go.result = GameObject.Instantiate(pre.load.Result);
							value?.Invoke(go);
						}
					};
				}
			}
			remove
			{
				
			}
		}
		public object Current => result;

		public bool MoveNext()
		{
			if (result != null)
				return true;
			if (precache.load.IsDone)
			{
				result = GameObject.Instantiate(precache.load.Result);
				return true;
			}

			return false;
		}
		
		public void Reset()
		{
		}
	}

	public class PrecacheItem
	{
		/// <summary>
		/// 当前数量
		/// </summary>
		public int count;

		/// <summary>
		/// 总计数量，统计用
		/// </summary>
		public int totalCount;

		/// <summary>
		/// 加载时间，统计用
		/// </summary>
		public float time;

		public AsyncOperationHandle<GameObject> load;

		public Stack<GameObject> pool;

		/// <summary>
		/// 记录过去30秒栈内剩余最小值
		/// </summary>
		public int min = Int32.MaxValue;

		public float cycleTime = 0;
		
		private string m_Address;

		public PrecacheItem(string address)
		{
			m_Address = address;
			pool = new Stack<GameObject>();
		}

		internal void Add()
		{
			if (count == 0)
			{
				load = Addressables.LoadAssetAsync<GameObject>(m_Address);
				//if(load.Status == AsyncOperationStatus.Failed)
				//{
				//	OptionalLog.EditorError($"资源 {m_Address} 不存在！");
				//}
				//OptionalLog.EditorLog($"precache {m_Address}");
			}

			count++;
			totalCount++;
		}

		internal void Remove()
		{
			count--;
			if (count == 0)
			{
				Addressables.Release(load);
				//OptionalLog.EditorLog($"removecache {m_Address}");
			}
			else if (count < 0)
			{
				Debug.LogError($"{m_Address} remove 次数比add还多了");
				count = 0;
			}
		}
	}

	public class AsyncObjectPool
	{
		public delegate void GetList(List<string> list);


		Dictionary<string, PrecacheItem> m_Cache = new Dictionary<string, PrecacheItem>();

		public void Precache(GetList get)
		{
			List<string> list = ListPool<string>.Get();
			get(list);
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				Precache(list[i]);
			}

			ListPool<string>.Release(list);
		}

		/// <summary>
		/// 预加载prefab，模型 特效等
		/// </summary>
		/// <param name="address"></param>
		public void Precache(string address)
		{
			if (!m_Cache.TryGetValue(address, out PrecacheItem item))
			{
				item = new PrecacheItem(address);
				m_Cache.Add(address, item);
			}

			item.Add();
		}


		const float m_ReleaseInterval = 30;
		float m_Time;
		public float interval = 1;

		private void Update()
		{
			if (Time.time > m_Time + interval)
			{
				m_Time = Time.time;
				foreach (var item in m_Cache.Values)
				{
					if (item.count > 0)
					{
						item.time += interval;
					}

					int count = item.pool.Count;
					if (count >= 0)
					{
						item.cycleTime += interval;
						if(count < item.min)
							item.min = count;
					}
					if (item.cycleTime > m_ReleaseInterval)
					{
						for (int i = 0; i < item.min; i++)
						{
							//Destroy(item.pool.Pop());
						}
						item.cycleTime = 0;
						item.min = Int32.MaxValue;
					}
				}
			}
		}


		#region ObjectPool

		public InstantiateGO GetGO(string address)
		{
			InstantiateGO go;
			if (m_Cache.TryGetValue(address, out PrecacheItem precacheItem))
			{
				if (precacheItem.pool.Count > 0)
				{
					//取出一个对象
					GameObject obj = precacheItem.pool.Pop();
					obj.SetActive(true);
					go = new InstantiateGO() {result = obj};
				}
				else
					go = new InstantiateGO() {precache = precacheItem};
			}
			else
			{
				Precache(address);
				go = new InstantiateGO() {precache = m_Cache[address]};
			}
			return go;
		}

		public void RecycleGo(string address, GameObject go)
		{
			go.SetActive(false);
			if (m_Cache.TryGetValue(address, out PrecacheItem precacheItem))
			{
				precacheItem.pool.Push(go);
			}
			else
			{
				Precache(address);
				m_Cache[address].pool.Push(go);
			}
		}

		#endregion
	}
}

