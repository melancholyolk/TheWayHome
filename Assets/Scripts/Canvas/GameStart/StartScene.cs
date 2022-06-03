using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 转场脚本，开始游戏转场
/// 控制物体倾斜指定角度
/// </summary>
public class StartScene : MonoBehaviour
{
    public Image[] fadeImg;
    public Text[] fadeText;
    public Toggle[] btns;
    public float duration;

    public static AsyncOperation AsyncOperation;
    [SerializeField] private bool startChange = false;

    
    private float _timer = 0;

    private void Start()
    {
        AsyncOperation = SceneManager.LoadSceneAsync(1);
        AsyncOperation.allowSceneActivation = false;
        
    }

    private void Update()
    {
        if (startChange)
        {
            if (_timer >= duration)
            {
                _timer = 0;
                startChange = false;
                AsyncOperation.allowSceneActivation = true;
                return;
            }
            _timer += Time.deltaTime;
            FadeOut(_timer);
        }
    }

    private void FadeOut(float timer)
    {
        foreach (var o in fadeImg)
        {
            if (o.GetComponent<Image>())
            {
                Color color = o.color;
                color.a = Mathf.Lerp(1, 0, timer / duration);
                o.color = color;
            }
        }

        foreach (var t in fadeText)
        {
            if (t.GetComponent<Text>())
            {
                Color color = t.GetComponent<Text>().color;
                color.a = Mathf.Lerp(1, 0, timer / duration);
                t.GetComponent<Text>().color = color;
            }
        }
    }

    public void StartConvert()
    {
        startChange = true;
    }
}