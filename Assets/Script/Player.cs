using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPosition;
    public float fireRate;
    public float maxHealth;
    
    public float health;
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
            Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
            nextFire = fireRate;
            anim.SetTrigger("Shoot");
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

    void DoDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && alive)
        {
            anim.SetTrigger("Death");
            AliveOrNot(false);

            sceneLoaderVar.LoadScene(0, 3f);
            

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
    }

}






