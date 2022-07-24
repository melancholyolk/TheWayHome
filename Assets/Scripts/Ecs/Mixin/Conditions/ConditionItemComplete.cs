using UnityEngine;

namespace Decode
{
	public class ConditionItemComplete : Conditions
	{
		public Item item;
		public override bool Accept()
		{
			return item.disable;
		}
	}
}