using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public class ConditionInput : Conditions
	{
		public KeyCode input;
		public bool isConsist;

		public enum InputResult
		{
			True,//满足所有条件
			False,//不满足checkinput
			InputFalse//满足checkinut不满足input
		}
		public override bool Accept()
		{
			if (!isConsist)
				return Input.GetKeyDown(input);
			else
				return Input.GetKey(input);
		}

		public override InputResult CheckInput(KeyCode key)
		{
			return InputResult.False;
		}

		public override bool DoInput(KeyCode key)
		{
			return key == input;
		}
	}

	public class ConditionKeyBoardLettersInput : ConditionInput
	{
		public override InputResult CheckInput(KeyCode key)
		{
			if ((int) key >= 97 && (int) key <= 122)
			{
				if (DoInput(key))
					return InputResult.True;
				else
					return InputResult.InputFalse;
			}
			return InputResult.False;
		}
	}
}