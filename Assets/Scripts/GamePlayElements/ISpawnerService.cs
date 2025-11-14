using UnityEngine;

public interface ISpawnerService : IService
{
    void RegisterSpawner(ISpawner spawner);
    void SendReqest(SpawnerType spawnerType, Vector3 position, int damage = 1, EntityType type = EntityType.Generic);
    void EnableSpawner(SpawnerType spawnerType);
    void DisableSpawn(SpawnerType spawnerType);
    void DestroySpawners();
}