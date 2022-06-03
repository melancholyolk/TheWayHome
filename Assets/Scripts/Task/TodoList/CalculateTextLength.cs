using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateTextLength : MonoBehaviour
{
    public float length;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float CalcTextWidth(Text text)
    {
        TextGenerator tg = text.cachedTextGeneratorForLayout;
        TextGenerationSettings setting = text.GetGenerationSettings(Vector2.zero);
        float width = tg.GetPreferredWidth(text.text, setting) / text.pixelsPerUnit;
        Debug.Log("width = " + width.ToString());
        return width;
    }
   
}
