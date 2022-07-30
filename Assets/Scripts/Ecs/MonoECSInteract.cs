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
		public static MonoECSInteract Instance;

		[SerializeField]
		private List<ObtainItems> m_ObItems;

		private Dictionary<string, Item> m_Items = new Dictionary<string, Item>();
		private void Awake()
		{
			Instance = this;
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
				if(!item.isUsing)
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
		}
		#endregion
	}
}