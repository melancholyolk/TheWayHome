using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 控制下一关场景加载
/// </summary>
public class SceneControl : MonoBehaviour
{
    public int index;

    public static AsyncOperation Async;

    private int _total = 5; //关卡总数（包括初始场景）


    // Start is called before the first frame update
    void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        if (++index <= _total)
        {
            Async = SceneManager.LoadSceneAsync(index);
            Async.allowSceneActivation = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Async.allowSceneActivation = true;
            GameObject.Find("CanvasTransition").GetComponent<SceneTransition>().LoadNextScene();
        }

    }
}