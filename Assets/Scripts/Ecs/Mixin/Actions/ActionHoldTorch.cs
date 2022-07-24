namespace Decode
{
	public class ActionHoldTorch:Actions
	{
		public override void DoAction()
		{
			CanvasManager.Instance.player.GetComponent<PlayerMove>().CmdHoldObject(0);
		}

		
	}
}