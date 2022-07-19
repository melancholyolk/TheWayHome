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
		public Conditions[] conditions;
		public Actions[] actions;
		private bool m_Result = false;
		public override bool Accept()
		{
			foreach (var condition in conditions)
			{
				if (!condition.Accept())
				{
					foreach (var action in actions)
					{
						action.SyncAction();
					}
					return false;
				}
			}
			return true;
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
						action.SyncAction();
					}
					return ConditionInput.InputResult.InputFalse;
				}
			}

			m_Result = true;
			return ConditionInput.InputResult.True;
		}
	}
}

