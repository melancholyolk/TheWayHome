using UnityEngine;

namespace Decode
{
	public class ActionFire:ActionChangeObjectState
	{
		public int order;
		public Event_Altar altar;
		public ParticleSystem fire;
		public Light light;
		public AudioSource audioSource;
		public Animator animator;
		
		private ParticleSystem.EmissionModule _emissionModule;
		public override void Init(string itemid,string id, Config config)
		{
			base.Init(itemid,id, config);
			_emissionModule = fire.emission;
		}

		public override void DoAction()
		{
			_emissionModule.enabled = true;
			light.enabled = true;
			animator.enabled = true;
			animator.SetTrigger("show");
			audioSource.Play();
			
			altar.CheckFire(order);
		}
	}
}