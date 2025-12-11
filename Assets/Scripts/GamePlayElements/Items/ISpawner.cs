using UnityEngine;

public interface ISpawner
{
    SpawnerType GetSpawnerType();

    void Spawn(HealthConfig config, Vector3 position, int count);
    void EnableSpawn();
    void DisableSpawn();
    void DestroyPool();
}