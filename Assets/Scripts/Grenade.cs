﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float Damage = 50;

    public float Delay = 3;
    public GameObject ExplosionPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        Invoke("Explosion", Delay);
    }
    private void Explosion()
    {
        Destroy(gameObject);
        var explosion = Instantiate(ExplosionPrefab);
        explosion.transform.position = transform.position;
        explosion.GetComponent<Explosion>().Damage = Damage;
    }
}
