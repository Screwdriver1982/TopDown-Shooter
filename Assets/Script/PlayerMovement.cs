using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.up);
        //Debug.Log(transform.right);
        //Debug.Log(transform.forward);

        Move();
        Rotate();

    }

    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(inputX, inputY) * speed;

        //Vector3 newPosition = transform.position;
        //newPosition.x += speed * Time.deltaTime * inputX;
        //newPosition.y += speed * Time.deltaTime * inputY;
        //transform.position = newPosition;
        //или можно по-другому
        // transform.position = new Vector3 (speed, 0 ,0);

    }

    void Rotate()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 direction = mouseWorldPosition - (Vector2)transform.position;
        transform.up = -direction;
    
    }
}
