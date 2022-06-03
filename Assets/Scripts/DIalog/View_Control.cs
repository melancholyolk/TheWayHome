using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class View_Control : MonoBehaviour
{
    [SerializeField] public Dialog_View dialogView;
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     dialogView = transform.GetChild(0).GetComponent<Dialog_View>();
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     timer += Time.deltaTime;
    //     if (Input.GetKeyDown(KeyCode.F))
    //     {
    //         
    //         dialogView.ShowView();
    //         // if (!isLocalPlayer)
    //         // {
    //         //     // gameObject.SetActive(false);
    //         //     return;
    //         // }
    //        // GetComponent<NetWorkDialog>().CmdOpenDialogView(0);
    //     }
    //
    //     // if (Input.GetKeyDown(KeyCode.B))
    //     // {
    //     //     StartCoroutine(dialogView.HideView());
    //     // }
    //     //
    //     // if (Input.GetKeyDown(KeyCode.C))
    //     // {
    //     //     dialogView.ChangeOri();
    //     // }
    //     //
    //     // if (Input.GetKeyDown(KeyCode.D))
    //     // {
    //     //     dialogView.ChangeTar();
    //     // }
    //     // if (Input.GetKeyDown(KeyCode.N))
    //     // {
    //     //     dialogView.NextSentence();
    //     // }
    // }

    public void ShowDialog(List<string> dialogs)
    {
        var player = GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>();
        int index = (int) player.player_type - 1;
        CharacterResources resources = GetComponent<CharacterResources>();
        List<MyDialogForm> mydialogs = new List<MyDialogForm>();
        foreach (var d in dialogs)
        {
            mydialogs.Add(new MyDialogForm(0,d));
        }
        dialogView.SetDialogList(mydialogs);
        if (!dialogView.gameObject.activeSelf)
        {
            dialogView.ori = resources.characters[index];
            dialogView.tar = resources.characters[1-index];
            dialogView.ShowView();
        }
    }
}