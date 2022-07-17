using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
    public class DisposableTrap : TrapConfig
    {
		private bool isUsed = false;
		public override void DoActions()
		{
			if(!isUsed)
			{
				isUsed = true;
				for(int i = 0; i < actions.Length; i++)
					actions[i].SyncAction();
			}
		}
	}
}