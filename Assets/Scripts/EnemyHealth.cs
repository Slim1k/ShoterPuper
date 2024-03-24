using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public PlayerProgress PlayerProgress_;
    public float Value = 100;
    public Animator AnimatorEnamy;
    public float DelayEnamiesTime = 10;

    private void Start()
    {
        PlayerProgress_ = FindObjectOfType<PlayerProgress>();
    }

    public bool IsAlive()
    {
        return Value > 0;
    }

    public void DestroyEnamy()
    {
        EnamyDeath();
    }

    public void DestroyEnamyFull()
    {
        EnamyDeath();
        Invoke("Destroy", DelayEnamiesTime);
    }
    public void DestroyEnamyFull(float delayEnamiesTime)
    {
        EnamyDeath();
        Invoke("Destroy", delayEnamiesTime);
    }

    private void EnamyDeath()
    {
        AnimatorEnamy.SetTrigger("death");

        GetComponent<EnemyAI>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        var explosion = GetComponent<ExplosionEnamy>();
        explosion.ExplosionEn();
    }

    public void DealDamage(float damage)
    {
        Value -= damage;
        AnimatorEnamy.SetTrigger("hit");

        PlayerProgress_.AddExperience(damage);

        if (Value <= 0)
        {
            DestroyEnamy();
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}