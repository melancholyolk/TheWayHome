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

		private Dictionary<string, ObtainItems> m_Items;
		private void Awake()
		{
			Instance = this;
			m_ObItems = new List<ObtainItems>();
			m_Items = new Dictionary<string, ObtainItems>();
		}
		
		// Update is called once per frame
		void Update()
		{
			
			
			foreach (var item in m_ObItems)
			{
				if (Input.anyKeyDown)
				{
					foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
					{
						if (Input.GetKeyDown(keyCode))
						{
							item.CheckKeyInput(keyCode);
						}
					}
				}
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

		public void AddGScript(string id,ObtainItems item)
		{
			m_Items.Add(id,item);
		}

		#endregion

		#region Cmd
		[Command]
		public void CmdComplete(string id)
		{
			RpcComplete(id);
		}
		[ClientRpc]
		public void RpcComplete(string id)
		{
			m_Items[id].isCompleted = true;
		}
		[Command]
		public void CmdIsUsing(string id, bool temp)
		{
			RpcIsUsing(id, temp);
		}
		[ClientRpc]
		public void RpcIsUsing(string id, bool temp)
		{
			m_Items[id].isUsing = temp;
		}

		#endregion
	}
}