using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Decode
{
	public class ObtainConfig
	{
		public string name;
		public Conditions[] conditions;
		public Actions[] actions;
		
		protected virtual void Complete(DecodeCallBack callBack)
		{
			callBack.CallBack();
		}

		public void CheckKeyInput(KeyCode key)
		{
			foreach (var condition in conditions)
			{
				if(!condition.KeyInput(key))
					return;
			}
		}

		public bool DoConditions()
		{
			foreach (var condition in conditions)
			{
				if(!condition.Accept())
					return false;
			}

			foreach (var action in actions)
			{
				action.CheckDoAction();
			}

			return true;
		}
	}
}

