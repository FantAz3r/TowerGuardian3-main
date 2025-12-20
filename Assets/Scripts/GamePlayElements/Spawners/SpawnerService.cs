using System.Collections.Generic;
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

    public void SendReqest(SpawnerType spawnerType, HealthConfig config, Vector3 position, int count = 1 )
    {
        if (_spawners.TryGetValue(spawnerType, out var spawner))
        {
            spawner.Spawn(config, position, count);
        }
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

    public void DestroySpawners()
    {
        foreach(var pair in _spawners)
        {
            pair.Value.DestroyPool();
        }
    }

   
}
