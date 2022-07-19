using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 获得道具
	/// </summary>
	public class ActionGetProp : Actions
	{
		public int PropertyID;
		public float size;
		// public CanvasManager.Player player = CanvasManager.Instance.player_type;

		public override void DoAction()
		{
			//获取道具实例
			CanvasManager.Instance.PickUpStart(PropertyID, size);
			//把道具给player
		}
	}
}
