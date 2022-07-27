namespace Decode
{
	public class ConditionOperationPlayCG:Conditions
	{
		public override bool Accept()
		{
			return OperationControl.Instance.is_playingCG;
		}
	}
}