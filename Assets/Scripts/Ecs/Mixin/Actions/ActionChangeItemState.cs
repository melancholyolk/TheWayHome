using System;
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

		public int configIndexer;
		public ItemState State = ItemState.IsUing;
		public override void DoAction()
		{
			var item = MonoECSInteract.Instance.GetItem(itemId) as ObtainItems;
			var r = item.configs[configIndexer];
			switch (State)
			{
					
				case ItemState.IsUing:
					
					r.isUsing = !r.isUsing;
					break;
				case ItemState.Disable:
					r.disable = !r.disable;
					break;
			}
		}
	}
}

