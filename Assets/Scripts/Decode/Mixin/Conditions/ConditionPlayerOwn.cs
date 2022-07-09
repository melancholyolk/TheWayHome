using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 判断玩家是否有该道具
	/// </summary>
	public class ConditionPlayerOwn : Conditions
	{
		public int PropertyID;
		public PlayerControl.Player playerType;

		public override bool Accept()
		{
			return false;
		}
	}

}
