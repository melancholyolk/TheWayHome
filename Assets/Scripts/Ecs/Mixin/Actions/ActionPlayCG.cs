using UnityEngine;
using UnityEngine.Video;

namespace Decode
{
	public class ActionPlayCG:Actions
	{
		public VideoPlayer cg;
		public Actions[] actions;
		public override void DoAction()
		{
			cg.gameObject.SetActive(true);
			cg.loopPointReached += (vp) => { vp.playbackSpeed = vp.playbackSpeed / 10.0F;vp.gameObject.SetActive(false);
				foreach (var action in actions)
				{
					action.SyncAction();
				}

				OperationControl.Instance.is_playingCG = false;
			};
			cg.Play();

			OperationControl.Instance.is_playingCG = true;
		}
	}
}