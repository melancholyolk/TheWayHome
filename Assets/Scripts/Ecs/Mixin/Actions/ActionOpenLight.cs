using UnityEngine;

namespace Decode
{
	public class ActionOpenLight:Actions
	{
		public Light light;
		public override void DoAction()
		{
			light.enabled = true;
		}
	}
}