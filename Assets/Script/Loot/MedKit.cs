﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MedKit : MonoBehaviour
{
    public float heal;

    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();

        if (player != null)
        {
            player.ChangeHP(heal);
            LeanPool.Despawn(gameObject);
        }
    }
}
