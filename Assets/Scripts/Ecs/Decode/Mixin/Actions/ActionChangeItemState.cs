using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 更改某个交互物体的状态
	/// </summary>
	public class ActionChangeItemState : Actions
	{
		public enum ItemState{
			IsUing,
			Disable
		}
		public ItemState State = ItemState.IsUing;
		public override void DoAction()
		{
			switch (State)
			{
				case ItemState.IsUing:
					MonoECSInteract.Instance.GetItem(itemId).isUsing = !MonoECSInteract.Instance.GetItem(itemId).isUsing;
					break;
				case ItemState.Disable:
					MonoECSInteract.Instance.GetItem(itemId).disable = !MonoECSInteract.Instance.GetItem(itemId).disable;
					break;
			}
		}
	}
}

