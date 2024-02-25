using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public float damage = 10;
    private void Start()
    {
        Invoke("DestroyFireball", LifeTime);
    }

    private void FixedUpdate()
    {
        FixedUpdateMove();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        DamageEnemy(collision);

        DestroyFireball();
    }

    private void DamageEnemy(Collision collision)
    {
        var enamyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enamyHealth != null)
        {
            enamyHealth.DealDamage(damage);
        }
    }

    private void FixedUpdateMove()
    {
        transform.position += transform.forward * Speed * Time.fixedDeltaTime;
    }

    private void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
