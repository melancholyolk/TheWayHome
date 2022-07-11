using System.Collections;
using System.Collections.Generic;
using Decode;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 洞穴房间4密码锁开启开启
	/// </summary>

	public class OnUnderGroundLockUnlocked : Actions
	{
		public Light[] Lights;
		public SpriteRenderer tar;
		public Sprite ori;

		public override void DoAction()
		{
			foreach (var VARIABLE in Lights)
			{
				VARIABLE.enabled = true;
			}

			tar.sprite = ori;
			tar.GetComponent<JudgePlayer>().enabled = true;
		}
		
	}
}

