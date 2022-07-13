using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Decode
{
	public class ActionSnowGroundElectricMachine : Actions
	{
		public GameObject[] lights;

		public Sprite lightOn;
		public Sprite lightOff;
		protected override void DoAction()
		{
			CmdOpenLights();
		}
		

		[Command(requiresAuthority = false)]
		void CmdOpenLights()
		{
			RpcOpenLights();
		}

		[ClientRpc]
		void RpcOpenLights()
		{
			foreach (var light in lights)
			{
				light.GetComponent<SpriteRenderer>().sprite = lightOn;
				light.GetComponentInChildren<Light>().enabled = true;
			}
			GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>().player.GetComponent<PlayerMove>().AddCondition("电力");
		}
	}

}
