
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ConnectButton : MonoBehaviour
{
    NetworkManager network;
    public GameObject btn_conn;
    void Start()
    {
        network = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
    }

    public void CreateHost()
    {
        network.StartHost();
        HideConnectButton();
    }

    public void JoinGame()
    {
        network.StartClient();
        HideConnectButton();
    }

    private void HideConnectButton()
    {
        btn_conn.SetActive(false);
    }
    public void ReturnMainInterface()
    {
        SceneManager.LoadScene("OfflineScene");
    }

}
