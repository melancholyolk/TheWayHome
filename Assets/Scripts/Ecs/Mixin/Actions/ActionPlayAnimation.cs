using UnityEngine;

namespace Decode
{
	public class ActionPlayAnimation :Actions
	{
		public Animator animator;

		public override void DoAction()
		{
			animator.enabled = true;
		}
	}
}