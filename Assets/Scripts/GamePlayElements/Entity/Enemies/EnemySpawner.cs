using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private IReadOnlyList<SpawnerActivator> _spawnPoints;
    private DayCycle _dayCycle;
    private Player _player;
    private LevelConfig _currentConfig;
    private Coroutine _spawnRoutine;
    private Coroutine _waveRoutine;

    private WaitForSeconds _nightSpawnDelay;
    private WaitForSeconds _daySpawnDelay;
    private WaitForSeconds _waveDuration;

    private List<Wave> _waves;
    private int _currentWaveIndex = 0;

    private Dictionary<Enemy, ObjectPool<Enemy>> _pools = new Dictionary<Enemy, ObjectPool<Enemy>>();
    private Dictionary<Enemy, float> _startWaights = new();
    private Dictionary<Enemy, float> _tempWaights = new();


    public void Init(Player player, DayCycle dayCycle, LevelConfig config, List<SpawnerActivator> spawnPoints)
    {
        _player = player;
        _dayCycle = dayCycle;
        _currentConfig = config;
        _spawnPoints = spawnPoints;

        _nightSpawnDelay = new WaitForSeconds(_currentConfig.NightSpawnDelay);
        _daySpawnDelay = new WaitForSeconds(_currentConfig.DaySpawnDelay);
        _waves = config.Waves;

        SetupPoolsAndWeightsForWave(_waves[_currentWaveIndex]);
    }

    private void SetupPoolsAndWeightsForWave(Wave wave)
    {
        _startWaights = wave.Weight.ToDictionary(kv => kv.Key, kv => (float)kv.Value);
        _waveDuration = new WaitForSeconds(_waves[_currentWaveIndex].Duration);

        foreach (var enemy in wave.Weight.Keys)
        {
            if (_pools.ContainsKey(enemy) == false)
                _pools[enemy] = new ObjectPool<Enemy>(enemy, 0, true);
        }
    }

    private void Spawn()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0)
            return;

        Enemy chosenEnemy = ChooseEnemyWithPseudoRandom();

        if (_pools.TryGetValue(chosenEnemy, out var pool) == false)
        {
            pool = new ObjectPool<Enemy>(chosenEnemy, 0, true);
            _pools[chosenEnemy] = pool;
        }

        Enemy enemyInstance = pool.Get();

        var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        enemyInstance.transform.position = spawnPoint.transform.position;
        enemyInstance.transform.LookAt(_player.transform);

        var stateMachine = enemyInstance.GetComponent<EnemyStateMachine>();
        stateMachine.Init(_player);
    }

    public void SetWave(int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= _waves.Count)
        {
            Debug.LogError("Некорректный индекс волны");
            return;
        }

        SetupPoolsAndWeightsForWave(_waves[_currentWaveIndex]);
    }

    public void StartSpawn()
    {
        if (_spawnRoutine == null)
            _spawnRoutine = StartCoroutine(SpawnRoutine());

        if (_waveRoutine == null)
            _waveRoutine = StartCoroutine(WaveRoutine());
    }

    public void StopSpawn()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (enabled)
        {
            Spawn();
            yield return (_dayCycle.CurrentPhase == DayPhase.Day) ? _daySpawnDelay : _nightSpawnDelay;
        }
    }

    private IEnumerator WaveRoutine()
    {
        while (enabled)
        {
            SetWave(_currentWaveIndex);
            yield return _waveDuration;
            _currentWaveIndex++;
        }
    }

    private Enemy ChooseEnemyWithPseudoRandom()
    {
        if (_startWaights.Count == 1)
            return _startWaights.Keys.First();

        if (_tempWaights.Count == 0)
        {
            Enemy enemy = Utils.SelectAndUpdateWeights(_startWaights, _startWaights, out Dictionary<Enemy, float> newWeights);
            _tempWaights = newWeights;
            return enemy;
        }
        else
        {
            Enemy enemy = Utils.SelectAndUpdateWeights(_startWaights, _tempWaights, out Dictionary<Enemy, float> newWeights);
            _tempWaights = newWeights;
            return enemy;
        }
    }
}