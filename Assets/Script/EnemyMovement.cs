using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Player player;

    Rigidbody2D rb;
    Vector3 playerPosition;
    bool alive;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        alive = GetComponent<AliveOrDeath>().alive;
        DetectPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        alive = GetComponent<AliveOrDeath>().alive;
        if (alive)
        {
            DetectPlayer();
            RotateTo(playerPosition);
        }
    }

    void RotateTo(Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        transform.up = -direction;
    }
    void DetectPlayer()
    {
        player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerPosition = player.transform.position;
        }
        
    }
}
