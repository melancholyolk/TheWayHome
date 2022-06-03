using UnityEngine;
using UnityEngine.UI;

public class IconsControl : MonoBehaviour
{
    private void Start()
    {
        //HideIcons();
        ShowIcons();
    }

    public void HideIcons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }

    public void ShowIcons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().enabled = true;
        }
    }
}