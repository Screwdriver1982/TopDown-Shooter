using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationHelper : MonoBehaviour
{
    Zombie zombie;
    // Start is called before the first frame update
    void Start()
    {
        zombie = GetComponentInParent<Zombie>();
    }

    void Attack()
    {
        zombie.DoDamageToPlayer();
    }
}
