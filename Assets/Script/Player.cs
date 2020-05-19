using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lean.Pool;


public class Player : MonoBehaviour
{
    public Action onHealthChanged = delegate { };
    public Action onDeath = delegate { };

    public GameObject bulletPrefab;
    public Transform shootPosition;
    public float fireRate;
    public float maxHealth;
    public float health;
    public int ammo;
    
    float nextFire;
    Animator anim;
    SceneLoader sceneLoaderVar;
    AliveOrDeath aliveVar;
    bool alive;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        aliveVar = GetComponent<AliveOrDeath>();
        AliveOrNot(true);
        anim = GetComponentInChildren<Animator>();
        sceneLoaderVar = FindObjectOfType<SceneLoader>();

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && nextFire<=0 && alive)
        {
            if (ammo > 0)
            {
                LeanPool.Spawn(bulletPrefab, shootPosition.position, transform.rotation);
                nextFire = fireRate;
                ammo -= 1;
                anim.SetTrigger("Shoot");


            }
            else
            {
                MissFire();
            }
        }

        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        
        }

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
        onHealthChanged();
        if (health <= 0 && alive)
        {
            anim.SetTrigger("Death");
            AliveOrNot(false);
            onDeath();
            

        }
    }

    private void AliveOrNot(bool aliveSet)
    {
        alive = aliveSet;
        aliveVar.SetAlive(aliveSet);
    }

    public void ChangeHP(float hpBonus)
    {
        health = Mathf.Clamp(health + hpBonus, 0, maxHealth);
        onHealthChanged();
    }

    void MissFire()
    { 
    
    }

    public void ChangeAmmo(int deltaAmmo)
    {
        ammo = Mathf.Max(0, ammo + deltaAmmo);
    }
}






