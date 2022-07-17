using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
    public class ActionChangeHp : Actions
    {
        public enum ChangeType{
            Loss,
            Recovery
		}
        public ChangeType type = ChangeType.Loss;
        
        public float num;
        public override void DoAction()
        {
            var target = CanvasManager.Instance.player;
            switch(type)
            {
                case ChangeType.Loss:
                    target.ChangeHp(-num);
                    break;
                case ChangeType.Recovery:
                    target.ChangeHp(num);
                    break;
			}

        }
    }
}