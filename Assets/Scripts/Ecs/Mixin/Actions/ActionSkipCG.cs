using UnityEngine.Video;

namespace Decode
{
	public class ActionSkipCG:Actions
	{
		public VideoPlayer cg;
		public override void DoAction()
		{
			
			// cg.loopPointReached += (vp) => { vp.playbackSpeed = vp.playbackSpeed / 10.0F;vp.gameObject.SetActive(false); };
			cg.time = cg.clip.length;
			OperationControl.Instance.is_playingCG = false;
		}
	}
}