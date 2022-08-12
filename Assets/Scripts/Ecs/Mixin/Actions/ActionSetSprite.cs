using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Decode
{
	public class ActionSetSprite : ActionChangeObjectState
	{
		public SpriteRenderer renderer;
		public Sprite sprite;

		public override void DoAction()
		{
			base.DoAction();
			renderer.sprite = sprite;
		}
	}
}

