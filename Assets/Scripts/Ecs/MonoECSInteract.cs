using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Decode
{
	/// <summary>
	/// 获取循环
	/// </summary>
	public class MonoECSInteract : NetworkBehaviour
	{
		public static MonoECSInteract Instance
		{
			get
			{
				if (!m_Instance)
				{
					m_Instance = GameObject.FindObjectOfType<MonoECSInteract>();
				}

				return m_Instance;
			}
		}

		[SerializeField]
		private List<ObtainItems> m_ObItems;
		private Dictionary<string, Item> m_Items = new Dictionary<string, Item>();

		private static MonoECSInteract m_Instance;
		private void Awake()
		{
			// m_ObItems = new List<ObtainItems>();
		}
		
		// Update is called once per frame
		void Update()
		{
			foreach (var item in m_ObItems)
			{
				// if (Input.anyKeyDown)
				// {
				// 	foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
				// 	{
				// 		if (Input.GetKeyDown(keyCode))
				// 		{
				// 			item.CheckKeyInput(keyCode);
				// 		}
				// 	}
				// }
				// if(!item.isUsing)
				item.CheckAllConfigs();
			}
		}


		#region publicAPI

		public void AddScript(ObtainItems item)
		{
			m_ObItems.Add(item);
		}

		public void RemoveScript(ObtainItems item)
		{
			m_ObItems.Remove(item);
		}

		public void AddGScript(string id,Item item)
		{
			if(m_Items is null)
				m_Items = new Dictionary<string, Item>();
			m_Items.Add(id,item);
		}

		public Item GetItem(string id)
		{
			m_Items.TryGetValue(id, out var v);
			return v;
		}

		public List<string> GetAll()
		{
			List<string> items = new List<string>();
			foreach (var VARIABLE in m_Items)
			{
				items.Add(VARIABLE.Value.id);
			}

			return items;
		}

		/// <summary>
		/// 设置obtainitem状态
		/// </summary>
		public void SetObtainItems(List<ObtainItemInfo> infos)
		{
			foreach (var info in infos)
			{
				if (m_Items.TryGetValue(info.id,out var item))
				{
					var obtain = item as ObtainItems;
					var configs = obtain.configs;
					for (int i = 0; i < configs.Count; i++)
					{
						configs[i].isComplete = info.configInfos[i].isComplete;
						configs[i].isUsing = info.configInfos[i].isUsing;
						configs[i].disable = info.configInfos[i].disable;
					}
				}
				else
				{
					Debug.LogError($"OtainItem信息{info}没有找到对应的id");
				}
			}
		}

		public List<ObtainItemInfo> GetObtainItems()
		{
			List<ObtainItemInfo> infos = new List<ObtainItemInfo>();
			foreach (var pair in m_Items)
			{
				Debug.Assert(!String.IsNullOrEmpty(pair.Key),pair.Value);
				ObtainItemInfo info = new ObtainItemInfo();
				info.configInfos = new List<ConfigInfo>();
				info.id = pair.Key;
				var v = pair.Value as ObtainItems;
				for (int i = 0; i < v.configs.Count; i++)
				{
					ConfigInfo ci = new ConfigInfo();
					ci.disable = v.configs[i].disable;
					ci.isComplete = v.configs[i].isComplete;
					ci.isUsing = v.configs[i].isUsing;
					info.configInfos.Add(ci);
				}
				infos.Add(info);
			}
			return infos;
		}
		#endregion

		#region Cmd

		[Command(requiresAuthority = false)]
		public void CmdAction(string launchId, string actionId, string targetId)
		{
			RpcAction(launchId, actionId ,targetId);
		}
		[ClientRpc]
		public void RpcAction(string launchId, string actionId, string targetId)
		{
			var launch = m_Items[launchId];
			launch.DoAction(actionId ,targetId);
			Debug.Log(launchId + " 1 " + actionId + " 2 " + targetId);
		}
		#endregion
	}
	
	public struct ObtainItemInfo
	{
		public string id;
		public List<ConfigInfo> configInfos;
	}

	public struct ConfigInfo
	{
		public bool isComplete;
		
		public bool isUsing;
		
		public bool disable;
	}
}