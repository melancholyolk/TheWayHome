using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 加载游戏菜单
/// </summary>
public class LoadStart : MonoBehaviour
{
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        GameObject.Find("CanvasTransition").GetComponent<SceneTransition>().LoadStartScene();
        GameObject.Find("Icons").GetComponent<IconsControl>().HideIcons();
        GameObject.Find("StopToggle").SendMessage("Run");
    }

    
}
