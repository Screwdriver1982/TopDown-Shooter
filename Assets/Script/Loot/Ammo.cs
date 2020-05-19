using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Ammo : MonoBehaviour
{
    public int ammo;

    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.ChangeAmmo(ammo);
            LeanPool.Despawn(gameObject);
        }
    }
}
