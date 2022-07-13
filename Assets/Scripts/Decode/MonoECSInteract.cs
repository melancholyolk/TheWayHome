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
	public class MonoECSInteract : SerializedMonoBehaviour
	{
		public static MonoECSInteract Instance;

		private CanvasManager m_CanvasManager;

#if UNITY_EDITOR
		[Sirenix.OdinInspector.ShowInInspector]
#endif
		private List<ObtainItems> m_ObItems;

		private void Awake()
		{
			Instance = this;
			m_ObItems = new List<ObtainItems>();
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

		#endregion
	}
}