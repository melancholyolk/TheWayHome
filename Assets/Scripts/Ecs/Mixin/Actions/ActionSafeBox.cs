using UnityEngine;

namespace Decode
{
	public class ActionSafeBox:Actions
	{
		public Light[] lights;
		public override void DoAction()
		{
			foreach (var light in lights)
			{
				light.enabled = true;
			}
		}
	}
}