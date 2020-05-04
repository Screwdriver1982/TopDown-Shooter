using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [Header("Item config")]
    public float health;

    [Header("Blast config")]
    public bool isExploding;
    public float blastRadius;
    public float blastDamage;
    public Animator anim;
    public float destroyTime;


    bool alive = true;
    
        
        
        // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            DoDamage(damageDealer.damage);
        }



    }

    public void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && alive)
        {
            alive = false;
            if (isExploding)
            {
                Blast();
            }
            Destroy(gameObject, destroyTime);
        }
    }

    void Blast()
    {
        anim.SetTrigger("Blast");
        LayerMask layerMask = LayerMask.GetMask("Player","Enemy");
        Collider2D[] objectInRadius = Physics2D.OverlapCircleAll(transform.position, blastRadius, layerMask);


        foreach (Collider2D objectI in objectInRadius)
        {
            if(objectI.gameObject == gameObject)
            {
                continue; //пойдет на следующую итерацию цикла
            }

            Zombie zombie = objectI.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.DoDamage(blastDamage);
                continue;
            }
            
            Enemy enemy = objectI.GetComponent<Enemy>();
             if (enemy != null)
            {
                enemy.DoDamage(blastDamage);
                continue;
            }
            
            Barrel barrel = objectI.GetComponent<Barrel>();
             if (barrel != null)
            {
                barrel.DoDamage(blastDamage);
                continue;
            }

            Player player = objectI.GetComponent<Player>();
                  
            if (player != null)
            {
                player.DoDamage(blastDamage);

            }
           
           
        }



    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
