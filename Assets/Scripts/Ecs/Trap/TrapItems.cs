using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
    public class TrapItems : Item
    {

        [ListDrawerSettings(ShowIndexLabels = true)]
        [Searchable]
        public List<TrapConfig> configs;

		public override void Awake()
		{
			base.Awake();
			for (int i = 0; i < configs.Count; i++)
			{
				configs[i].Awake(id,i.ToString());
			}
		}

		private void Update()
		{
			for(int i = 0; i < configs.Count; i++)
            {
                configs[i].Update();
			}
		}

		public override void DoAction(string actionId, string targetId)
		{
			for (int i = 0; i < configs.Count; i++)
			{
				configs[i].DoActions(actionId, targetId);
			}
		}
	}
}
