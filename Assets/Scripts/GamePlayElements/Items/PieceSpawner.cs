using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PieceSpawner : BaseSpawner
{
    private Dictionary<ResourceType, ObjectPool<ResourcePiece>> _pools = new Dictionary<ResourceType, ObjectPool<ResourcePiece>>();
    private ResourceData _data;

    private readonly float _ejectForceMin = 5f;
    private readonly float _ejectForceMax = 10f;
    private readonly float _ejectRadius = 2f;
    public override SpawnerType GetSpawnerType() => SpawnerType.Resources;

    public PieceSpawner(ResourceData data)
    {
        _data = data;

        foreach (var resource in _data.ResourceInfos)
        {
            if (_pools.ContainsKey(resource.Type) == false)
            {
                _pools.Add(resource.Type, new ObjectPool<ResourcePiece>(resource.Prefab, 0, true));
            }
        }
    }

    public override void Spawn(HealthConfig config, Vector3 position, int count = 0)
    {
        if (CanSpawn == false)
            return;

        if (_pools.TryGetValue(config.SpawnResource, out var pool) == false)
            return;

        for (int i = 0; i < count; i++)
        {
            ResourcePiece piece = pool.Get();
            piece.transform.position = CreateSpawnPoint(position);

            piece.TryGetComponent(out Rigidbody rigidbody);
            Vector3 ejectDirection = Random.onUnitSphere;
            ejectDirection.y = Mathf.Abs(ejectDirection.y);
            ejectDirection.Normalize();

            float force = Random.Range(_ejectForceMin, _ejectForceMax);
            rigidbody.AddForce(ejectDirection * force, ForceMode.Impulse);
        }
    }

    public override void DestroyPool()
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
