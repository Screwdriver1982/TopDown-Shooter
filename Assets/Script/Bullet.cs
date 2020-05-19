using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void OnEnable()
    {
        rb.velocity = -transform.up*speed;
        
    }
    private void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        { 
            LeanPool.Despawn(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AliveOrDeath aliveOrDeath = collision.GetComponent<AliveOrDeath>();

        if (aliveOrDeath != null)
        {
            if (aliveOrDeath.alive)
            {
                LeanPool.Despawn(gameObject);
            }

        }
        else
        {
            LeanPool.Despawn(gameObject);
        }
    }
}
