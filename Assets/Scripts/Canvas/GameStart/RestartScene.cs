using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 重新开始当前关卡
/// </summary>
public class RestartScene : MonoBehaviour
{
    
    public void Restart()
    {
        print("Restart!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameObject.Find("CanvasTransition").GetComponent<SceneTransition>().LoadNextScene();
        GameObject.Find("Icons").GetComponent<IconsControl>().HideIcons();
    }
}
