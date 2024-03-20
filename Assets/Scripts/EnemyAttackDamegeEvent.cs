using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDamegeEvent : MonoBehaviour
{
    public EnemyAI EnemyAI_;
    public void AttackDamageEvent()
    {
        EnemyAI_.AttackDamage();
    }
}
