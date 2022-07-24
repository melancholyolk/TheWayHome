using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DoorControl : NetworkBehaviour
{
	public bool is_v = false;

	private bool can_use = false;

	public bool is_positive = false;

	public Material material;

	// Update is called once per frame
	void Update()
	{
		if (can_use)
		{
			if (is_positive)
			{
				if (is_v)
				{
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
					material.SetInt("_ZWrite", 1);
					material.DisableKeyword("_ALPHATEST_ON");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = -1;
				}
				else
				{
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
					material.SetInt("_ZWrite", 0);
					material.DisableKeyword("_ALPHATEST_ON");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = 3000;
				}
			}
			else
			{
				if (is_v)
				{
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
					material.SetInt("_ZWrite", 0);
					material.DisableKeyword("_ALPHATEST_ON");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = 3000;
				}
				else
				{
					material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
					material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
					material.SetInt("_ZWrite", 1);
					material.DisableKeyword("_ALPHATEST_ON");
					material.DisableKeyword("_ALPHABLEND_ON");
					material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					material.renderQueue = -1;
				}
			}

		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			can_use = true;
			if (is_v)
			{
				if (other.transform.position.x - this.transform.position.x > 0)
				{
					is_positive = true;
				}
				else
				{
					is_positive = false;
				}
			}
			else
			{
				if (other.transform.position.z - this.transform.position.z > 0)
				{
					is_positive = true;
				}
				else
				{
					is_positive = false;
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.tag.Equals("Player"))
		{
			can_use = false;
		}
	}
}
