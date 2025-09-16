using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _levelConfigs;  
    [SerializeField] private Enemy _enemy;

    private Transform _player;
    private ObjectPool<Enemy> _pool;
    private DayCycle _dayCycle;

    private float _minSpawnDistance;
    private float _maxSpawnDistance;
    private float _nightSpawnDelay;
    private float _daySpawnDelay;

    private WaitForSeconds _nightDelayWait;
    private WaitForSeconds _dayDelayWait;
    private LevelConfig _currentConfig;

    public void Init(Transform player, DayCycle dayCycle, LevelID level)
    {
        _player = player;
        _dayCycle = dayCycle;

        foreach (var levelConfig in _levelConfigs)
        {
            if (levelConfig.Level == level)  
            {
                _currentConfig = levelConfig;
                break;
            }
        }

        _minSpawnDistance = _currentConfig.MinSpawnDistance;
        _maxSpawnDistance = _currentConfig.MaxSpawnDistance;
        _nightSpawnDelay = _currentConfig.NightSpawnDelay;
        _daySpawnDelay = _currentConfig.DaySpawnDelay;

        _nightDelayWait = new WaitForSeconds(_nightSpawnDelay);
        _dayDelayWait = new WaitForSeconds(_daySpawnDelay);
        _pool = new ObjectPool<Enemy>(_enemy);

        StartCoroutine(SpawnRoutine());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Spawn()
    {
        if (_player == null || _enemy == null)
            throw new ArgumentNullException(nameof(_enemy), "EnemySpawner: Ќе заполнен список врагов или игрока отсутствует.");

        Vector3 spawnPos = GetPosition();
        _enemy = _pool.Get();
        _enemy.transform.position = spawnPos;
        IDemageable health = _enemy.GetComponent<IDemageable>();
        health.Died += OnDied;
    }

    private void OnDied(Health health)
    {
        Enemy enemy = health.GetComponent<Enemy>();
        _pool.Release(enemy);
    }

    private Vector3 GetPosition()
    {
        float angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
        float distance = UnityEngine.Random.Range(_minSpawnDistance, _maxSpawnDistance);
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
        Vector3 spawnPosition = _player.position + offset;

        return spawnPosition;
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            if (_dayCycle.CurrentPhase == DayPhase.Day)
            {
                yield return _dayDelayWait;
            }
            else
            {
                yield return _nightDelayWait;
            }

            Spawn();
        }
    }
}