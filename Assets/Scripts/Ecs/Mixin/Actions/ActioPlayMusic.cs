using UnityEngine;

namespace Decode
{
	public class ActionPlayMusic : ActionChangeObjectState
	{
		public AudioSource audioSource;
		public AudioClip clip;
		public override void DoAction()
		{
			base.DoAction();
			audioSource.Stop();
			audioSource.loop = true;
			audioSource.clip = clip;
			audioSource.Play();
		}
	}
}