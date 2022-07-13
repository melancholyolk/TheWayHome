using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 更改某个交互物体的状态
	/// </summary>
	public class ActionChangeObtainItemState : Actions
	{
		public ObtainItems item;
		public bool isUsing;
		public bool isCompleted;

		public ActionChangeObtainItemState(){}
		public ActionChangeObtainItemState(bool isUsing = false, bool isCompleted = false)
		{
			this.isCompleted = isCompleted;
			this.isUsing = isUsing;
		}
		protected override void DoAction()
		{
			item.isCompleted = isCompleted;
			item.isUsing = isUsing;
		}
	}
}

