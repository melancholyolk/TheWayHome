using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 获取循环
	/// </summary>
	public class MonoECSInteract : NetworkBehaviour
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

		// Start is called before the first frame update
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			foreach (var item in m_ObItems)
			{
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