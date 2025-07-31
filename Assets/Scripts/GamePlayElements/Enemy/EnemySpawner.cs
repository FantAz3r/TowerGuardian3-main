using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _minSpawnDistance = 10f;
    [SerializeField] private float _maxSpawnDistance = 40f;
    [SerializeField] private List<Enemy> _enemies;

    private Transform _player;
    private EnemyFactory _factory;

    public void Init(Transform player)
    {
        _player = player;
    }

    private void Awake()
    {
        _factory = new EnemyFactory();
    }

    public void SpawnRandomEnemy()
    {
        int randomIndex = Random.Range(0, _enemies.Count);
        Enemy enemyPrefab = _enemies[randomIndex];

        _factory.Create(GetPosition(),enemyPrefab);
    }

    private Vector3 GetPosition()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float distance = Random.Range(_minSpawnDistance, _maxSpawnDistance);
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        Vector3 spawnPosition = _player.position + offset;

        if (Physics.Raycast(spawnPosition + Vector3.up * 10f, Vector3.down, out RaycastHit hit, 20f))
        {
            spawnPosition.y = hit.point.y;
        }

        return spawnPosition;
    }
}
