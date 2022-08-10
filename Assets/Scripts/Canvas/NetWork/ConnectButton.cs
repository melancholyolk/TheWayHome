using Mirror;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Scriptable;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectButton : MonoBehaviour
{
    public NetworkManager network;
    public GameObject uiConnect;
    public GameObject uiRoomList;
    public GameObject uiHostRoom;
    public GameObject uiClient;
    public GameObject uiNewGame;
    public GameObject uiLoadGame;
    public GameObject uiBack;
    public GameObject uiSaves;
    public InputField field;
    public Text text;
	public GameInfoSave[] saves;
    private string m_SavePath = "Assets/Res/ScriptableObjects/Resources";
    void Start()
    {
	    text.text = "本机IP ： " + GetLocalIP();
    }

    public void CreateRoom()
    {
	    uiHostRoom.SetActive(false);
	    uiClient.SetActive(false);
	    uiNewGame.SetActive(true);
	    uiLoadGame.SetActive(true);
	    uiBack.SetActive(true);
    }

    public void BackToRoom()
    {
	    uiHostRoom.SetActive(true);
	    uiClient.SetActive(true);
	    uiNewGame.SetActive(false);
	    uiLoadGame.SetActive(false);
	    uiBack.SetActive(false);
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

    public void CreateNewSave()
    {
	    DirectoryInfo dirInfo = new DirectoryInfo(m_SavePath);
	    if (!dirInfo.Exists)
	    {
		    Debug.LogError(string.Format("can found path={0}", m_SavePath));
		    return;
	    }

	    string scriptableObjectName = "GameSave_" + DateTime.Now.Millisecond;
	    // ScriptableObject对象要用ScriptableObject.CreateInstance创建
	    var ddata = ScriptableObject.CreateInstance(nameof(GameInfoSave));
	    string json = JsonUtility.ToJson(ddata,true);
	    Debug.LogError(json);
	    // 创建一个asset文件
	    string assetPath = string.Format("{0}/{1}.asset", m_SavePath, scriptableObjectName);
#if UNITY_EDITOR
	    AssetDatabase.CreateAsset(ddata, assetPath);
#endif

	    GameState.currentSave = ddata as GameInfoSave;
	    Debug.Log("Finish!");
    }

    
    private void HideConnectButton()
    {
        uiConnect.SetActive(false);
    }
    public void ReturnMainInterface()
    {
        SceneManager.LoadScene("OfflineScene");
    }

    public void ShowSaves()
    {
	    uiSaves.GetComponent<Animator>().SetTrigger("show");
    }
    public void HideSaves()
    {
	    uiSaves.GetComponent<Animator>().SetTrigger("hide");
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
