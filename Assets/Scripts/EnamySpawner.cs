using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamySpawner : MonoBehaviour
{
    public bool IncreaseEnamiesMax = false;
    public EnemyAI EnemyPrefab;
    public PlayerController Player;
    public List<Transform> PatrolPoints;
    public bool DeleteEnamiesForTime = false;
    public float DeleteEnamiesTime = 10;

    public int EnamiesMaxCount = 5;
    public float Delay = 5;
    public float IncreaseEnamiesCountDelay = 30;

    private List<Transform> _spawnerPoints;
    private List<EnemyAI> _enemies;

    private float _timeLastSpawned;

    private void Start()
    {
        _spawnerPoints = new List<Transform>(transform.GetComponentsInChildren<Transform>());
        _enemies = new List<EnemyAI>();

        if (IncreaseEnamiesMax)
        {
            Invoke("IncreaseEnemiesMaxCount", IncreaseEnamiesCountDelay);
        }
    }

    private void IncreaseEnemiesMaxCount()
    {
        EnamiesMaxCount++;
        Invoke("IncreaseEnemiesMaxCount", IncreaseEnamiesCountDelay);
    }

    private void Update()
    {
        CheckForDeadEnemies();
        CreateEnamy();
    }

    private void CheckForDeadEnemies()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].IsAlive()) continue;
            var enamy = _enemies[i];
            _enemies.RemoveAt(i);

            if (DeleteEnamiesForTime)
                enamy.DestroyEnamyFull(DeleteEnamiesTime);
            
            i--;
        }
    }

    private void CreateEnamy()
    {
        if (_enemies.Count >= EnamiesMaxCount) return;
        if (Time.time - _timeLastSpawned < Delay) return;

        var enemy = Instantiate(EnemyPrefab);
        enemy.transform.position = _spawnerPoints[Random.Range(0, _spawnerPoints.Count)].position;
        enemy.Player = Player;
        enemy.PatrolPoints = PatrolPoints;
        _enemies.Add(enemy);
        _timeLastSpawned = Time.time;
    }
}
