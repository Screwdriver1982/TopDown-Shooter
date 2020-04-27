using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public float health;
    public GameObject bulletPrefab;
    public Transform shootPosition;
    public float fireRate;

    float nextFire;
    Animator anim;
    AliveOrDeath aliveVar;
    bool alive;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        aliveVar = GetComponent<AliveOrDeath>();
        AliveOrNot(true);


    }

    // Update is called once per frame
    void Update()
    {
        //оставляю два ифа, т.к. в стрельбу скорее всего будет добавлено еще какое-то условие типа расстояния до цели, агресия и т.п.

        if (nextFire <= 0 && alive)
        {
            Atack();
        } 
        
        if(nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        }
    }

    void Atack()
    {
        Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
        nextFire = fireRate;
        anim.SetTrigger("Atack");
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
            anim.SetTrigger("Death");
            AliveOrNot(false);


        }
    }

    private void AliveOrNot(bool aliveSet)
    {
        alive = aliveSet;
        aliveVar.SetAlive(aliveSet);
    }


}
