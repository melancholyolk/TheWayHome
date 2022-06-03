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

    private CanvasManager _canvasManager;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        _canvasManager = GetComponent<CanvasManager>();
        network = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Escape) && (_canvasManager.CanOperate() ||  _canvasManager.is_setting))
        {
            if (time < 1f) return;
            if (!esc_menu.activeSelf)
            {
                _canvasManager.is_setting = true;
                esc_menu.SetActive(!esc_menu.activeSelf);
            }
            else
            {
                escmenu_show = true;
                _canvasManager.is_setting = false;
            }
            time = 0;
        }

        if (escmenu_show)
        {
            if (time > 1)
            {
                time = 0;
                escmenu_show = false;
                esc_menu.SetActive(!esc_menu.activeSelf);
            }
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