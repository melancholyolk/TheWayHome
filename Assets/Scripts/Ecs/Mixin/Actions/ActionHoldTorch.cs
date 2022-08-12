namespace Decode
{
	public class ActionHoldTorch:ActionChangeObjectState
	{
		public override void DoAction()
		{
			base.DoAction();
			CanvasManager.Instance.player.GetComponent<PlayerMove>().CmdHoldObject(0);
		}

		
	}
}