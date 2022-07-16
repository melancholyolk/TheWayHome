using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	public class ObtainConfig : Config
	{
		public string name;
		public Conditions[] conditions;
		

		protected virtual void Complete(DecodeCallBack callBack)
		{
			callBack.CallBack();
		}
		

		public void CheckKeyInput(KeyCode key)
		{
			foreach (var condition in conditions)
			{
				if (condition is not ConditionFeedBack) continue;
				var feedback = (ConditionFeedBack) condition;
				if (feedback.CheckInput(key) != ConditionInput.InputResult.True) continue;
				foreach (var action in actions)
				{
					//action.CheckDoAction();
				}
			}
		}

		public bool DoConditions()
		{
			foreach (var condition in conditions)
			{
				if (!condition.Accept())
					return false;
			}

			foreach (var action in actions)
			{
				action.SyncAction();
			}

			return true;
		}
	}
}

