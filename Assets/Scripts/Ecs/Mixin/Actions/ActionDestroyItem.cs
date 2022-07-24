using UnityEngine;
namespace Decode
{
	public class ActionDestroyItem : Actions
	{
		public override void DoAction()
		{
			var item = MonoECSInteract.Instance.GetItem(itemId) as ObtainItems;
			foreach (var decode in item.decodes)
			{
				decode.OnInterrupt();
			}

			m_Config.isComplete = false;
		}
	}
}