using Mirror;
using System.Collections;
using FogOfWar;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerChose : NetworkBehaviour
{
    [SyncVar]
    public int player = 0;
    public GameObject[] player_prefab;
    private bool is_server = false;
    [FormerlySerializedAs("m_player")]
    public PlayerTest playerTest;
    [SyncVar]
    public bool is_ready = false;
    private bool is_complete = false;

    private Camera camera;

    public GameObject prop_manager;

    private void Start()
    {
        camera = GameObject.FindWithTag("DecodeCamera").GetComponent<Camera>();
    }

    [Command]
    public void CmdPlayerChose(int index)
    {
        if (player != 0)
            GameObject.Find("Button" + player).GetComponent<ChoseButton>().ChangeState(false, false);
        if (index != 0)
        {
            GameObject.Find("Button" + index).GetComponent<ChoseButton>().ChangeState(true, is_server);
        }
        player = index;
        PrcPlayerChose(index);
    }
    [ClientRpc]
    private void PrcPlayerChose(int index)
    {
        player = index;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (!is_ready && !is_complete)
        {
            PlayerCreate();
        }
        else if (is_ready && !is_complete)
        {
            CmdPlayerSame();
            is_complete = true;

        }
        else if(!playerTest && GameObject.Find("Player" + player + "(Clone)"))
        {
            playerTest = GameObject.Find("Player" + player + "(Clone)").GetComponent<PlayerTest>();
            GameObject.Find("TaskLoader").GetComponent<TaskManager>().InitTaskLoader(player);
            playerTest.gameObject.AddComponent<AudioListener>();
            playerTest.gameObject.AddComponent<FowViewer>();
            playerTest.gameObject.GetComponent<FowViewer>().viewerRange = 20;
        }

        if (playerTest)
        {
            playerTest.tag = "Player";
            Camera.main.GetComponent<CameraFollow>().SetPlayer(playerTest.transform);
            GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>().player = playerTest.GetComponent<PlayerTest>();
            if(player == 1)
            {
                GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>().player_type = CanvasManager.Player.Player1;
            }
            else
            {
                GameObject.FindWithTag("Canvas").GetComponent<CanvasManager>().player_type = CanvasManager.Player.Player2;
            }
            playerTest.is_local = true;
            Destroy(this.gameObject);
        }
    }

    private void PlayerCreate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.transform.tag == "Button")
            {
                if (isServer)
                {
                    is_server = true;
                }
                if (hit.transform.name.Equals("Button1") && !hit.transform.GetComponent<ChoseButton>().GetState())
                {
                    CmdPlayerChose(1);
                }
                else if (hit.transform.name.Equals("Button2") && !hit.transform.GetComponent<ChoseButton>().GetState())
                {
                    CmdPlayerChose(2);
                }
                else
                {
                    CmdPlayerChose(0);
                }
            }
        }
    }

    [Command]
    public void CmdPlayerSame()
    {
        GameObject obj = GameObject.Instantiate(player_prefab[player - 1]);
        NetworkServer.Spawn(obj, GetComponent<NetworkIdentity>().connectionToClient);
    }
}
