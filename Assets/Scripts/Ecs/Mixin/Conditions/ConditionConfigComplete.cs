using UnityEngine;
namespace Decode
{
	public class ConditionConfigComplete : Conditions
	{
		public override bool Accept()
		{
			return m_Config.isComplete;
		}
	}
}