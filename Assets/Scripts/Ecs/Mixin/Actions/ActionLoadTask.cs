namespace Decode
{
	public class ActionLoadTask : ActionChangeObjectState
	{
		public TaskManager taskManager;
		public override void DoAction()
		{
			base.DoAction();
			taskManager.InitTaskLoader((int)CanvasManager.Instance.player_type);
		}
	}
}