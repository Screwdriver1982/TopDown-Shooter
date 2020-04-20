using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    bool alive;
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        alive = GetComponent<AliveOrDeath>().alive;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        alive = GetComponent<AliveOrDeath>().alive;
        if (alive)
        { 
            Move();
            Rotate();
        }

    }

    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(inputX, inputY) * speed;
        
        anim.SetFloat("Speed",rb.velocity.magnitude);



    }

    void Rotate()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 direction = mouseWorldPosition - (Vector2)transform.position;
        transform.up = -direction;
    
    }
}
