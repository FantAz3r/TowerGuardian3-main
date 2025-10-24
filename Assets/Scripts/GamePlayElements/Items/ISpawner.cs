using UnityEngine;

public interface ISpawner
{
    SpawnerType GetSpawnerType();

    void Spawn(EntityType type, Vector3 position, int count);
    void EnableSpawn();
    void DisableSpawn();
    void DestroyPool();
}