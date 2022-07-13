using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public class ActionPlayAudio : Actions
	{
		public AudioSource audio;
		public AudioClip clip;

		protected override void DoAction()
		{
			audio.PlayOneShot(clip);
		}
	}
}

