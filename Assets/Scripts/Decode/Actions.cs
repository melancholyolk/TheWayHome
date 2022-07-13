using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 动作
	/// </summary>
	public abstract class Actions
	{
		public bool needDone;
		[ReadOnly]
		[ShowInInspector]
		protected bool isDone = false;
		public virtual void CheckDoAction()
		{
			if (!needDone)
			{
				if (!isDone)
				{
					DoAction();
					isDone = true;
				}
			}
			else
			{
				DoAction();
			}
		}
		public virtual void DoAction()
		{
			
		}
	}
}

