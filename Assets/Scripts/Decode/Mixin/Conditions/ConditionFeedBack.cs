using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public class ConditionFeedBack : Conditions
	{
		public ConditionInput[] conditions;
		public Actions[] actions;
		private bool m_Result = false;
		public override bool Accept()
		{
			return m_Result;
		}

		public override bool KeyInput(KeyCode key)
		{
			foreach (var condition in conditions)
			{
				if (!condition.KeyInput(key))
				{
					foreach (var action in actions)
					{
						action.CheckDoAction();
					}

					return false;
				}
			}

			m_Result = true;
			return true;
		}
	}
}

