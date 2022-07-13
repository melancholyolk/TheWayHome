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
		public virtual void CheckInput()
		{
        
		}

		public virtual void DoActions()
		{
			foreach (var action in actions)
			{
				action.DoAction();
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

