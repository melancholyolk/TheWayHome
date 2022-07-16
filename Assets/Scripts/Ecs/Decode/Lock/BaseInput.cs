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
	public class BaseInput : SerializedMonoBehaviour
	{
		public Actions[] actions;
		private ObtainItems m_Parent;
		

		public void Initialize(ObtainItems parent)
		{
			m_Parent = parent;
		}


		
		public virtual void CheckInput()
		{
			
		}

		public virtual void DoActions()
		{
			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].SyncAction();
			}
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