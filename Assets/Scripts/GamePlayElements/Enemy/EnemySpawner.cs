using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    private IReadOnlyList<SpawnerActivator> _spawnPoints;
    private ISpawnerService _spawnerService;
    private ObjectPool<Enemy> _pool;
    private DayCycle _dayCycle;
    private Transform _player;
    private LevelConfig _currentConfig;

    private WaitForSeconds _nightSpawnDelay;
    private WaitForSeconds _daySpawnDelay;

    public void Init(Transform player, DayCycle dayCycle, LevelConfig config, ISpawnerService spawnerService)
    {
        _player = player;
        _spawnerService = spawnerService;
        _dayCycle = dayCycle;
        _currentConfig = config;

        _spawnPoints = _currentConfig.SpawnPointContainer.SpawnPoints;
        _nightSpawnDelay = new WaitForSeconds(_currentConfig.NightSpawnDelay);
        _daySpawnDelay = new WaitForSeconds(_currentConfig.DaySpawnDelay);

        _pool = new ObjectPool<Enemy>(_enemyPrefab, 0, true);

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            Spawn();

            if (_dayCycle.CurrentPhase == DayPhase.Day)
                yield return _daySpawnDelay;
            else
                yield return _nightSpawnDelay;
        }
    }

    private void Spawn()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
        {
            return;
        }

        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        Enemy enemyInstance = _pool.Get();

        EnemyStateMachine stateMachine = enemyInstance.GetComponent<EnemyStateMachine>();
        enemyInstance.transform.position = spawnPoint.transform.position;
        enemyInstance.transform.LookAt(_player.transform);
        stateMachine.Init(_player.transform);

        SpawnbleEntity spawnbleEntity = GetComponent<SpawnbleEntity>();
        spawnbleEntity.Init(_spawnerService);
    }
}