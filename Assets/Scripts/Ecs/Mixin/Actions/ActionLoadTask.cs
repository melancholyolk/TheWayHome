namespace Decode
{
	public class ActionLoadTask : Actions
	{
		public TaskManager taskManager;
		public override void DoAction()
		{
			taskManager.InitTaskLoader((int)CanvasManager.Instance.player_type);
		}
	}
}