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
    private List<GameObject> clues = new List<GameObject>();

    public void OpenClue()
    {
        GetPropInfo();
    }
    private void GetPropInfo()
    {
        foreach (GameObject clue in clues)
        {
            Destroy(clue);
        }
        prop_info = SyncManager.Instance.GetPropInfo();
        for(int i = 0; i < prop_info.Count; i++)
        {
            GameObject obj = Instantiate(panel_prefab, this.transform);
            obj.GetComponent<SetInfo>().SetProp(prop_info[i],describe);
            clues.Add(obj);
        }
    }

}
