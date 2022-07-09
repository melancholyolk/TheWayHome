using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 解密循环
	/// </summary>
	public class MonoECSDecode : MonoBehaviour
	{
		public KeyCode input;

		public static MonoECSDecode Instance;
		
		private CanvasManager m_CanvasManager;

		#if UNITY_EDITOR
		[ShowInInspector]
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
			m_CanvasManager = GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>();
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(input))
			{
				// if (m_CanvasManager.CanOperate())
				// {
					foreach (var item in m_ObItems)
					{
						item.CheckAllConfigs();
					}
				// }
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

