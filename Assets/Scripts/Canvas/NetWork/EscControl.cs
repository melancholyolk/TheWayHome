using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscControl : NetworkBehaviour
{
	NetworkManager network;
	public GameObject esc_menu;

	private bool escmenu_show = false;
	
	// Start is called before the first frame update
	void Start()
	{
		network = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) &&
		    (OperationControl.Instance.CanOperate() || OperationControl.Instance.is_setting))
		{
			if (!esc_menu.activeSelf)
			{
				OperationControl.Instance.is_setting = true;
				esc_menu.SetActive(!esc_menu.activeSelf);
			}
			else
			{
				escmenu_show = true;
				OperationControl.Instance.is_setting = false;
			}
		}

		if (escmenu_show)
		{
			escmenu_show = false;
			esc_menu.SetActive(!esc_menu.activeSelf);
		}
	}

	public void EndConnect()
	{
		if (isServer)
		{
			EndHostConnect();
		}
		else
		{
			EndClientConnect();
		}

		SceneManager.LoadScene("ConnectScene");
	}

	private void EndHostConnect()
	{
		network.StopHost();
	}

	private void EndClientConnect()
	{
		network.StopClient();
	}
}