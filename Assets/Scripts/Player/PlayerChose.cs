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
    [FormerlySerializedAs("playerTest")] [FormerlySerializedAs("m_player")]
    public PlayerMove playerMove;
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
        else if(!playerMove && GameObject.Find("Player" + player + "(Clone)"))
        {
            playerMove = GameObject.Find("Player" + player + "(Clone)").GetComponent<PlayerMove>();
            GameObject.Find("TaskLoader").GetComponent<TaskManager>().InitTaskLoader(player);
            playerMove.gameObject.AddComponent<AudioListener>();
            playerMove.gameObject.AddComponent<FowViewer>();
            playerMove.gameObject.GetComponent<FowViewer>().viewerRange = 20;
        }

        if (playerMove)
        {
            playerMove.tag = "Player";
            Camera.main.GetComponent<CameraFollow>().SetPlayer(playerMove.transform);
            CanvasManager.Instance.player = playerMove.GetComponent<PlayerMove>();
            if(player == 1)
            {
                CanvasManager.Instance.player_type = CanvasManager.Player.Player1;
            }
            else
            {
                CanvasManager.Instance.player_type = CanvasManager.Player.Player2;
            }
            playerMove.isLocal = true;
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
