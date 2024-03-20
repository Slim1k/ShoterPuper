﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Damage = 50;
    public float MaxSize = 5;
    public float Speed = 1;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        if (MaxSize > transform.localScale.x)
        {
            transform.localScale += Vector3.one * Time.deltaTime * Speed;
            return;
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        var playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.DealDamage(Damage);
        }

        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(Damage);
        }
    }
}
