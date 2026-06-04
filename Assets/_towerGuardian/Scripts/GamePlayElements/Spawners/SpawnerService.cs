using System.Collections.Generic;
using TowerGuardian.Enums;
using TowerGuardian.StaticData;
using UnityEngine;

public class SpawnerService : ISpawnerService
{
    private Dictionary<SpawnerType, ISpawner> _spawners = new Dictionary<SpawnerType, ISpawner>();

    public void RegisterSpawner(ISpawner spawner)
    {
        SpawnerType type = spawner.GetSpawnerType();

        if (_spawners.ContainsKey(type) == false)
            _spawners[type] = spawner;
    }

    public void SendItemReqest(HealthConfig config, Vector3 position, int count = 1)
    {
        if (_spawners.TryGetValue(SpawnerType.Resources, out var spawner))
        {
            spawner.Spawn(config, position, count);
        }
    }

    public void SendSoundReqest(AudioClip clip, Vector3 position = default)
    {
        if (_spawners.TryGetValue(SpawnerType.Sounds, out var spawner))
        {
            spawner.Spawn(clip, position);
        }
    }

    public void SendTextReqest(Vector3 position, int count = 1, Color? color = null)
    {
        if (_spawners.TryGetValue(SpawnerType.Text, out var spawner))
        {
            spawner.Spawn(position, count, color);
        }
    }

    public void SendEffectReqest(EffectType type, Vector3 position, Transform parent = null)
    {
        if (_spawners.TryGetValue(SpawnerType.Effects, out var spawner))
        {
            spawner.Spawn(type, position, parent);
        }
    }

    public Projectile SendProjectileRequest(ProjectileType type, Vector3 position, Transform parent = null)
    {
        if (_spawners.TryGetValue(SpawnerType.Projectile, out var spawner))
        {
            return spawner.Spawn(type, position, parent);
        }

        return null;
    }

    public void EnableSpawner(SpawnerType spawnerType)
    {
        if (_spawners.TryGetValue(spawnerType, out var spawner))
        {
            spawner.EnableSpawn();
        }
    }

    public void DisableSpawn(SpawnerType spawnerType)
    {
        if (_spawners.TryGetValue(spawnerType, out var spawner))
        {
            spawner.DisableSpawn();
        }
    }

    public void ClearObjects(SpawnerType spawnerType)
    {
        if (_spawners.TryGetValue(spawnerType, out var spawner))
        {
            spawner.ClearObjects();
        }
    }

    public void DestroySpawners()
    {
        foreach (var pair in _spawners)
        {
            pair.Value.DestroyPool();
        }
    }
}
