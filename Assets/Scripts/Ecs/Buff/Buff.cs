using UnityEngine;

namespace Decode.Buff
{
	public class Buff
	{
		public Buff()
		{
			LocalPlayerAtrributeDeal.Instance.AddListener(this);
		}
		
		public virtual void OnAdd()
		{
			
		}
		
		public virtual void Execute()
		{
			
		}

		public virtual void OnRemove()
		{
			
		}
	}
}