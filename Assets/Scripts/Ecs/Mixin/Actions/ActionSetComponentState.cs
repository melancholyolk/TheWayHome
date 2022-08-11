using UnityEngine;

namespace Decode
{
	public class ActionOpenLight<T>:Actions where T:Behaviour 
	{
		public T component;
		public override void DoAction()
		{
			component.enabled = true;
		}
	}
}