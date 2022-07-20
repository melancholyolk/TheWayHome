using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 判断自己是否正确
	/// 并执行action
	/// </summary>
	public class DecodeBaseInput : SerializedMonoBehaviour
	{
		public Actions[] actions;
		private ObtainItems m_Parent;
		private Config m_Config;

		public void Initialize(ObtainItems parent,Config config)
		{
			m_Parent = parent;
			m_Config = config;
		}


		
		public virtual void CheckInput()
		{
			
		}

		public virtual void OnInterrupt()
		{
			m_Config.isComplete = false;
			Destroy(gameObject);
		}

		public virtual void DoActions()
		{
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].SyncAction();
			}
			Destroy(gameObject);
		}

		/// <summary>
		/// 解密成功后调用
		/// </summary>
		protected virtual IEnumerator Success()
		{
			yield return null;
		}
	}
}