
using UnityEngine;
using Mirror;

public class NetWorkChange : NetworkManager
{
    NetworkConnection m_con;
    public override void OnClientConnect(NetworkConnection conn)
    {
        m_con = conn;
        base.OnClientConnect(conn);
    }
    public void MyNewObject()
    {
        GameObject go = null;
        if (spawnPrefabs.Count > 0)
        {
            for(int i = 0; i < spawnPrefabs.Count; i++)
            {
                go = spawnPrefabs[i];
                var player = (GameObject)GameObject.Instantiate(go);
                NetworkServer.Spawn(player);
                NetworkServer.AddPlayerForConnection(m_con, player);
            }
        } 
    }
}
