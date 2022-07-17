using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Decode
{
    public class ActionChangeWarm : Actions
    {
        public enum ChangeType
        {
            Loss,
            Recovery
        }
        public ChangeType type = ChangeType.Loss;

        public float num;
        [Sirenix.OdinInspector.ShowIf("type",ChangeType.Loss),Tooltip("°´ÕÕ±ÈÀýËðÊ§ÉúÃü")]
        public float rate;
        public override void DoAction()
        {
            var target = CanvasManager.Instance.player;
            switch (type)
            {
                case ChangeType.Loss:
                    target.LossWarm(-num);
                    break;
                case ChangeType.Recovery:
                    target.ChangeWarm(num);
                    break;
            }

        }
    }
}

