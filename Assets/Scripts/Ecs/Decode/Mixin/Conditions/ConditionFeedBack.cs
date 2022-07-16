using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 操作错误后会有反馈
	/// </summary>
	public class ConditionFeedBack : Conditions
	{
		public ConditionInput[] conditions;
		public Actions[] actions;
		private bool m_Result = false;
		public override bool Accept()
		{
			return m_Result;
		}

		public override ConditionInput.InputResult CheckInput(KeyCode key)
		{
			foreach (var condition in conditions)
			{
				if (condition.CheckInput(key) == ConditionInput.InputResult.False) 
					return ConditionInput.InputResult.False;
				if (condition.CheckInput(key) == ConditionInput.InputResult.InputFalse)
				{
					foreach (var action in actions)
					{
						action.CheckDoAction();
					}
					return ConditionInput.InputResult.InputFalse;
				}
			}

			m_Result = true;
			return ConditionInput.InputResult.True;
		}
	}
}

