using UnityEngine;

namespace Decode
{
	public class ActionCompleteTask:Actions
	{
		public string taskMessage;
		public override void DoAction()
		{
			GameObject.Find("TaskLoader").SendMessage("CmdCompleteTask", taskMessage);
		}
	}
}