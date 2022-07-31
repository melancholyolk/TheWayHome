namespace Decode
{
	public class ActionRemoveProp:Actions
	{
		public int propID;
		public override void DoAction()
		{
			CanvasManager.Instance.RemovePropInfo(propID);
		}
	}
}