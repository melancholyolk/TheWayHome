using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public class ActionDebug : Actions
	{
		public string logInfo;
		public override void DoAction()
		{
			Debug.Log(logInfo);
		}
	}

}
