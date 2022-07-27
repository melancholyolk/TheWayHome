namespace Decode
{
	public class ConditionGameState : Conditions
	{
		public GameState state;
		public GameState.Chapter chapter;
		public override bool Accept()
		{
			return state.state == chapter;
		}
	}
}