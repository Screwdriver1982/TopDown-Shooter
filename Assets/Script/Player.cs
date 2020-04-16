using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPosition;
    public float fireRate;

    float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && nextFire<=0)
        {
            Instantiate(bulletPrefab, shootPosition.position, transform.rotation);
            nextFire = fireRate;
        }

        if (nextFire > 0)
        {
            nextFire -= Time.deltaTime;
        
        }

    }
}
