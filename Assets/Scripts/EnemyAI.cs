using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> PatrolPoints;
    public PlayerController Player;
    public float ViewAngle;
    public float Damage = 30;
    public float DamagePerSecond = 1;
    public float AttackDistance = 1;
    public Animator AnimatorEnamy;

    private PlayerHealth _playerHealth;
    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;

    private void Start()
    {
        InitComponentLinks();
        PickNewPatrolPoint();
    }

    private void Update()
    {
        NoticePlayerUpdate();

        ChaseUpdate();

        PatrolUpdate();

        AttackUpdate();
    }

    private void AttackUpdate()
    {
        if (_isPlayerNoticed == true && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            AnimatorEnamy.SetTrigger("attack");
        }
    }

    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = PatrolPoints[Random.Range(0, PatrolPoints.Count)].position;
    }

    private void PatrolUpdate()
    {
            if (!_isPlayerNoticed)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                   PickNewPatrolPoint();
                }
            }
    }

    private void InitComponentLinks()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerHealth = Player.GetComponent<PlayerHealth>();
    }

    private void NoticePlayerUpdate()
    {
        _isPlayerNoticed = false;
        if (!_playerHealth.IsAlive()) return;

        var direction = Player.transform.position - transform.position;
        if (Vector3.Angle(transform.forward, direction) < ViewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == Player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
            }
        }
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = Player.transform.position;
        }
    }

    public void AttackDamage()
    {
        var enemyHealth = gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth.Value <= 0)
        {
            enemyHealth.DestroyEnamy();
            return;
        }
        if (!_isPlayerNoticed) return;
        if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance + AttackDistance) return;
        _playerHealth.DealDamage(Damage);
    }
}