
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectButton : MonoBehaviour
{
    NetworkManager network;
    public GameObject btn_conn;
    public InputField field;
    public Text text;
    void Start()
    {
        network = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
        text.text = "本机IP ： " + GetLocalIP();
    }

    public void CreateHost()
    {
        network.StartHost();
        HideConnectButton();
    }

    public void JoinGame()
    {
        if (!String.IsNullOrEmpty(field.text))
        {
            network.networkAddress = field.text;
        }
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
    public static string GetLocalIP()
    {
        try
        {
            string HostName = Dns.GetHostName(); //得到主机名
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {

                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return IpEntry.AddressList[i].ToString();
                }
            }
            return "";
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}
