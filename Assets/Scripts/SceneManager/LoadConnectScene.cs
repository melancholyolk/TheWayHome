using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadConnectScene : MonoBehaviour
{
    public void ConnectScene()
    {
        SceneManager.LoadScene("ConnectScene");
    }

    public void GameEnd()
    {
        Application.Quit();
    }
}
