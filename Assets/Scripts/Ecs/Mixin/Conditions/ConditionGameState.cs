namespace Decode
{
	public class ConditionGameState : Conditions
	{
		public GameState.Chapter chapter;
		public override bool Accept()
		{
			return GameState.state == chapter;
		}
	}
}