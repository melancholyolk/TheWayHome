using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5f;
    public bool is_local = true;
    private Rigidbody rigidbody;
    
    private PlayerManager playerManager;
    public enum Player
    {
        None,
        Player1,
        Player2
    }

    public Player player = Player.None;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
    }
    private void Update()
    {
        var moveX = Input.GetAxis("Horizontal");
        var moveY = Input.GetAxis("Vertical");

        Vector2 move_speed = new Vector2(moveX, moveY).normalized * speed;

        rigidbody.velocity = move_speed;
    }
}
