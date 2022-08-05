namespace Decode
{
	public class ActionSetLocalPlayerCondition : Actions
	{
		public int propId;
		public override void DoAction()
		{
			CanvasManager.Instance.player.AddCondition(propId);
		}
	}
}