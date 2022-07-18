using UnityEngine;
using UnityEngine.Video;

namespace Decode
{
	///播放视频
	public class ActionPlayVideo : Actions
	{
		public VideoPlayer VideoPlayer;
		public override void DoAction()
		{
			VideoPlayer.loopPointReached += (v) =>
			{
				v.gameObject.SetActive(false);
			};
			VideoPlayer.Play();
		}
	}
}