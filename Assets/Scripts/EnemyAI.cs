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

    private PlayerHealth _playerHealth;
    private NavMeshAgent _navMeshAgent;
    private bool _isPlayerNoticed;
    private float _timer;

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
        _timer += Time.deltaTime;
        if (_isPlayerNoticed == true && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && _timer >= DamagePerSecond)
        {
            _playerHealth.DealDamage(Damage);
            _timer = 0;
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
        var direction = Player.transform.position - transform.position;

        _isPlayerNoticed = false;
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
}