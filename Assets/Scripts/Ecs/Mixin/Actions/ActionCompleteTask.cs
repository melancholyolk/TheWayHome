using UnityEngine;

namespace Decode
{
	public class ActionCompleteTask:ActionChangeObjectState
	{
		public string taskMessage;
		public override void DoAction()
		{
			GameObject.Find("TaskLoader").SendMessage("CmdCompleteTask", taskMessage);
		}
	}
}