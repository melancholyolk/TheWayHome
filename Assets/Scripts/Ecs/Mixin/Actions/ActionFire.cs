using UnityEngine;

namespace Decode
{
	public class ActionFire:Actions
	{
		public int order;
		public Event_Altar altar;
		public ParticleSystem fire;
		public Light light;
		public AudioSource audioSource;
		public Animator animator;
		
		private ParticleSystem.EmissionModule _emissionModule;
		public override void Init(string id, Config config)
		{
			base.Init(id, config);
			_emissionModule = fire.emission;
		}

		public override void DoAction()
		{
			_emissionModule.enabled = true;
			light.enabled = true;
			animator.SetTrigger("show");
			audioSource.Play();
			
			altar.CheckFire(order);
		}
	}
}