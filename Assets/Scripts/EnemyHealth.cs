using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float Value = 100;
    public Animator AnimatorEnamy;

    public void DestroyEnamy()
    {
        EnamyDeath();
        //Destroy(gameObject);
    }

    private void EnamyDeath()
    {
        AnimatorEnamy.SetTrigger("death");

        GetComponent<EnemyAI>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    public void DealDamage(float damage)
    {
        Value -= damage;
        AnimatorEnamy.SetTrigger("hit");
        if (Value <= 0)
        {
            DestroyEnamy();
        }
    }
}