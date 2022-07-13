using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Decode
{
	public class ActionSetSprite : Actions
	{
		public Image image;
		public Sprite sprite;

		protected override void DoAction()
		{
			image.sprite = sprite;
		}
	}
}

