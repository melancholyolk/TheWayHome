using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ÏßË÷Õ¹Ê¾
/// </summary>
public class ClueShow : MonoBehaviour
{
    public GameObject panel_prefab;
    public Text describe;

    private List<PropInfo> prop_info = new List<PropInfo>();
    private SyncManager prop_manager;
    private List<GameObject> clues = new List<GameObject>();

    public void OpenClue()
    {
        if(!prop_manager)
        prop_manager = GameObject.FindWithTag("SyncManager").GetComponent<SyncManager>();
        GetPropInfo();
    }
    private void GetPropInfo()
    {
        foreach (GameObject clue in clues)
        {
            Destroy(clue);
        }
        prop_info = prop_manager.GetPropInfo();
        for(int i = 0; i < prop_info.Count; i++)
        {
            GameObject obj = Instantiate(panel_prefab, this.transform);
            obj.GetComponent<SetInfo>().SetProp(prop_info[i],describe);
            clues.Add(obj);
        }
    }

}
