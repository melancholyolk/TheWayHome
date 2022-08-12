using UnityEngine;

namespace Decode
{
	public class ActionSetComponentState:ActionChangeObjectState
	{
		public Behaviour component;
		public bool state;
		public override void DoAction()
		{
			base.DoAction();
			component.enabled = state;
		}
	}
}