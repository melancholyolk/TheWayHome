using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decode
{
	/// <summary>
	/// 播放对话
	/// </summary>
	public class ActionDialog : Actions
	{
		public View_Control dialogUI;
		public List<string> dialogs;
		public override void DoAction()
		{
			dialogUI.ShowDialog(dialogs);
		}
	}
}

