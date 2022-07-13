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
		private ActionChangeObtainItemState m_Change;
		private ObtainItems m_Parent;
		

		public void Initialize(ObtainItems parent)
		{
			m_Parent = parent;
			m_Change = new ActionChangeObtainItemState(true);
			m_Change.item = parent;
			m_Change.CheckDoAction();
		}

		private void OnEnable()
		{
			if(m_Change == null)
				return;
			m_Change.isUsing = true;
			m_Change.CheckDoAction();
		}

		public void OnDisable()
		{
			Debug.Assert(m_Change!=null,"检查Parent");
			m_Change.isUsing = false;
			m_Change.CheckDoAction();
		}

		public virtual void DoActions()
		{
			foreach (var action in actions)
			{
				action.CheckDoAction();
			}
		}


		public virtual void CheckInput()
		{
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