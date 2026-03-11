using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int NoGameLevelCount = 4;
    private IReadOnlyList<SpawnerActivator> _spawnPoints;
    private List<SpawnerActivator> _activeSpawnPoints = new();
    private DayCycle _dayCycle;
    private Player _player;
    private Coroutine _spawnRoutine, _waveRoutine;
    private WaitForSeconds _nightSpawnDelay, _daySpawnDelay, _waveDuration;
    private LevelConfig _levelConfig;
    private List<Wave> _waves;
    private int _currentWaveIndex = 0;

    private Dictionary<Enemy, ObjectPool<Enemy>> _pools = new Dictionary<Enemy, ObjectPool<Enemy>>();
    private Dictionary<Enemy, float> _startWaights = new();

    private IGameFactory _gameFactory;

    private void Awake()
    {
        _gameFactory = ServiceLocator.Get<IGameFactory>();
        _levelConfig = _gameFactory.LevelConfig;
        _player = _gameFactory.Player;
        _dayCycle = _gameFactory.Cycle;
        _spawnPoints = _gameFactory.SceneContainer.SpawnPoints;
        _waves = _levelConfig.Waves;

        foreach (var spawnPoint in _spawnPoints)
        {
            spawnPoint.Detected += AddSpawnPoint;
            spawnPoint.Losted += RemoveSpawnPoint;
        }

        SetupPoolsAndWeightsForWave(_waves[_currentWaveIndex]);
    }

    private void OnDestroy()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            spawnPoint.Detected -= AddSpawnPoint;
            spawnPoint.Losted -= RemoveSpawnPoint;
        }
    }

    private void SetupPoolsAndWeightsForWave(Wave wave)
    {
        _nightSpawnDelay = new WaitForSeconds(wave.NightSpawnDelay);
        _daySpawnDelay = new WaitForSeconds(wave.DaySpawnDelay);

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
        if (_activeSpawnPoints == null || _activeSpawnPoints.Count == 0)
            return;

        int totalActiveEnemies = 0;

        foreach (var item in _pools.Values)
        {
            totalActiveEnemies += item.GetActiveObjectsCount(); 
        }

        if (totalActiveEnemies >= _waves[_currentWaveIndex].MaxEnemyCount)
            return;

        Enemy chosenEnemy = GetRandomEnemy();

        if (_pools.TryGetValue(chosenEnemy, out var pool) == false)
        {
            pool = new ObjectPool<Enemy>(chosenEnemy, 0, true);
            _pools[chosenEnemy] = pool;
        }

        Enemy enemyInstance = pool.Get();

        var spawnPoint = _activeSpawnPoints[Random.Range(0, _activeSpawnPoints.Count-1)];
        enemyInstance.transform.position = spawnPoint.transform.position;
        enemyInstance.transform.LookAt(_player.transform);

        enemyInstance.Init(_player.transform, (int)_levelConfig.Level - NoGameLevelCount);
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

    private Enemy GetRandomEnemy()
    {
        return Utils.SelectByWeights(_startWaights);
    }

    private void AddSpawnPoint(SpawnerActivator spawner)
    {
        _activeSpawnPoints.Add(spawner);
    }

    private void RemoveSpawnPoint(SpawnerActivator spawner)
    {
        _activeSpawnPoints.Remove(spawner);
    }
}