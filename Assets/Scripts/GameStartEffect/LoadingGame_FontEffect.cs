using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LoadingGame_FontEffect : MonoBehaviour
{
    public GameObject text;

    public TextAsset loadGameFile;
    private int _max = 30;
    [FormerlySerializedAs("_current")] public int current = 0;

    List<string> linesOfAllFiles = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV_StagingDirection(loadGameFile);
        InvokeRepeating(nameof(InstantiateFont), 0, 0.5f);
    }

    private void Update()
    {
        
    }
    
    

    private void InstantiateFont()
    {
        if ( current >= _max) return;
        Vector3 pos = Random.insideUnitSphere;
        pos.z = Mathf.Clamp(pos.z, -1, 0);
        pos *= 100;
        var t = Instantiate(text, pos ,Quaternion.identity,transform);
        var textMeshPro = t.GetComponent<TextMeshPro>();
        string c = linesOfAllFiles[Random.Range(0, linesOfAllFiles.Count)];
        textMeshPro.SetText(c);
        t.AddComponent<MeshCollider>();
        t.AddComponent<FontInfo>();
        current ++;
    }

    private void ReadCSV_StagingDirection(TextAsset texts)
    {
        //去除空项
        string[] lines = texts.text.Split(new char[] {'\r', '\n'}, System.StringSplitOptions.RemoveEmptyEntries);

        linesOfAllFiles.AddRange(lines);
    }
}