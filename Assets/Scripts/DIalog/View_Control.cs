using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class View_Control : MonoBehaviour
{
    [SerializeField] 
    public Dialog_View dialogView;
	[SerializeField]
    public CharacterResource characterResource;
    public void ShowDialog(List<string> dialogs)
    {
        var player = CanvasManager.Instance;
        int index = (int) player.player_type - 1;
        List<MyDialogForm> mydialogs = new List<MyDialogForm>();
        foreach (var d in dialogs)
        {
            mydialogs.Add(new MyDialogForm(0,d));
        }
        dialogView.SetDialogList(mydialogs);
        if (!dialogView.gameObject.activeSelf)
        {
            dialogView.ori = characterResource.characters[index];
            dialogView.tar = characterResource.characters[1-index];
            dialogView.ShowView();
        }
    }
}