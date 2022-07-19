using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public class ConditionCheckLocalPlayer : Conditions
	{
		public int propId;
		public override bool Accept()
		{
			return CanvasManager.Instance.player.JudgeCondition(propId);
		}
	}
}

