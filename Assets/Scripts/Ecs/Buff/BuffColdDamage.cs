namespace Decode.Buff
{
	public class BuffColdDamage:Buff
	{
		public PlayerAttribute attribute;
		public float interval;
		public BuffColdDamage(float interval) : base()
		{
			attribute = CanvasManager.Instance.player.m_attribute;
		}
		public override void OnAdd()
		{
			
		}
	}
}