using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float velocityCheck;

    AliveOrDeath aliveOrDeath;
    Rigidbody2D rb;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        aliveOrDeath = GetComponent<AliveOrDeath>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        
        
        Move();
        Rotate();
        
    }

    void Move()
    {
        if (!aliveOrDeath.alive)
        {
            return;
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(inputX, inputY) * speed;
        velocityCheck = rb.velocity.magnitude;
        anim.SetFloat("Speed",rb.velocity.magnitude);



    }

    void Rotate()
    {
        if (!aliveOrDeath.alive)
        {
            return;
        }
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 direction = mouseWorldPosition - (Vector2)transform.position;
        transform.up = -direction;
    
    }
}
