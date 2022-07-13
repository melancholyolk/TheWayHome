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
		private Dictionary<string,ObtainItems> m_ObItems;

		private void Awake()
		{
			Instance = this;
			m_ObItems = new Dictionary<string, ObtainItems>();
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
							item.Value.CheckKeyInput(keyCode);
						}
					}
				}
				item.Value.CheckAllConfigs();
			}
		}


		#region publicAPI

		public void AddScript(string id,ObtainItems item)
		{
			m_ObItems.Add(id,item);
		}

		public void RemoveScript(string id)
		{
			m_ObItems.Remove(id);
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
			m_ObItems[id].isCompleted = true;
		}
		[Command]
		public void CmdIsUsing(string id, bool temp)
		{
			RpcIsUsing(id, temp);
		}
		[ClientRpc]
		public void RpcIsUsing(string id, bool temp)
		{
			m_ObItems[id].isUsing = temp;
		}

		#endregion
	}
}