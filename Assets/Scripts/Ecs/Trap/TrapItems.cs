using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
    public class TrapItems : Item
    {
        public List<Trap> TrapList = new List<Trap>();

        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name")]
        [Searchable]
        public List<TrapConfig> configs;

		private void Awake()
		{
			for (int i = 0; i < configs.Count; i++)
			{
				configs[i].Awake();
				configs[i].Init(this);
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
