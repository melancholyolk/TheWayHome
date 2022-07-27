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

		private Dictionary<string, Item> m_Items;
		private void Awake()
		{
			Instance = this;
			// m_ObItems = new List<ObtainItems>();
			m_Items = new Dictionary<string, Item>();
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
			m_Items.Add(id,item);
		}

		public Item GetItem(string id)
		{
			return m_Items[id];
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