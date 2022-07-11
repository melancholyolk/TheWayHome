using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	public class ConditionInput : Conditions
	{
		public KeyCode input;
		public bool isConsist;

		public override bool Accept()
		{
			if (!isConsist)
				return Input.GetKeyDown(input);
			else
				return Input.GetKey(input);
		}
	}
}