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
		protected override void DoAction()
		{
			MonoECSInteract.Instance.CmdComplete(item.Id);
		}
	}
}

