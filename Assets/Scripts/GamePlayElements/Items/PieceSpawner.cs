using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PieceSpawner : ISpawner
{
    private SpawnerType _type = SpawnerType.Resources;
    private Dictionary<ResourceType, ObjectPool<ResourcePiece>> _pools = new Dictionary<ResourceType, ObjectPool<ResourcePiece>>();
    private bool _spawning = true;

    private readonly float _ejectForceMin = 5f;
    private readonly float _ejectForceMax = 10f;
    private readonly float _ejectRadius = 2f;

    public SpawnerType GetSpawnerType() { return _type; }

    public PieceSpawner(ResourceData data)
    {
        foreach (var resource in data.ResourceInfos)
        {
            if (_pools.ContainsKey(resource.Type) == false)
            {
                _pools.Add(resource.Type, new ObjectPool<ResourcePiece>(resource.Prefab, 0, true));
            }
        }
    }

    public void EnableSpawn()
    {
        _spawning = true;
    }

    public void DisableSpawn()
    {
        _spawning = false;
    }

    public void Spawn(HealthConfig config, Vector3 position, int count = 0)
    {
        if (_spawning == false)
            return;

        if (_pools.TryGetValue(config.SpawnResource, out var pool) == false)
            return;

        for (int i = 0; i < count; i++)
        {
            ResourcePiece piece = pool.Get();
            piece.transform.position = CreateSpawnPoint(position);

            Rigidbody rb = piece.GetComponent<Rigidbody>();
            Vector3 ejectDirection = Random.onUnitSphere;
            ejectDirection.y = Mathf.Abs(ejectDirection.y);
            ejectDirection.Normalize();

            float force = Random.Range(_ejectForceMin, _ejectForceMax);
            rb.AddForce(ejectDirection * force, ForceMode.Impulse);
        }
    }

    public void DestroyPool()
    {
        foreach(var pair in _pools)
        {
            pair.Value.DestroyPool();
        }
    }

    private Vector3 CreateSpawnPoint(Vector3 origin)
    {
        Vector3 spawnPos = origin + Random.insideUnitSphere * _ejectRadius;
        spawnPos.y = origin.y;
        return spawnPos;
    }
}
