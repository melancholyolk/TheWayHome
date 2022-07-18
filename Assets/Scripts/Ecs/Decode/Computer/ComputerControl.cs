using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Decode
{
	public class ComputerControl : DecodeBaseInput
	{
		public Text text;
		public void Complete()
		{
			DoActions();
		}
		
		public void Wrong()
		{
			text.text = "";
		}

	}
}
