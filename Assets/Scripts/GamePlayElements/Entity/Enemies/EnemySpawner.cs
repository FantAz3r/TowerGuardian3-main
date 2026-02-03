using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    private IReadOnlyList<SpawnerActivator> _spawnPoints;
    private ObjectPool<Enemy> _pool;
    private DayCycle _dayCycle;
    private Player _player;
    private LevelConfig _currentConfig;
    private Coroutine _spawnRoutine;

    private WaitForSeconds _nightSpawnDelay;
    private WaitForSeconds _daySpawnDelay;

    public void Init(Player player, DayCycle dayCycle, LevelConfig config)
    {
        _player = player;
        _dayCycle = dayCycle;
        _currentConfig = config;

        _spawnPoints = _currentConfig.SpawnPointContainer.SpawnPoints;
        _nightSpawnDelay = new WaitForSeconds(_currentConfig.NightSpawnDelay);
        _daySpawnDelay = new WaitForSeconds(_currentConfig.DaySpawnDelay);

        _pool = new ObjectPool<Enemy>(_enemyPrefab, 0, true);

    }

    public void StartSpawn()
    {
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawn()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
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
        stateMachine.Init(_player);
    }
}