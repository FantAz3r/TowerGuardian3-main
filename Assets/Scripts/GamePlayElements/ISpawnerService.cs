using UnityEngine;

public interface ISpawnerService : IService
{
    void RegisterSpawner(ISpawner spawner);
    void SendReqest(SpawnerType spawnerType, EntityType type, Vector3 position, int damage);
    void EnableSpawner(SpawnerType spawnerType);
    void DisableSpawn(SpawnerType spawnerType);
    void DestroySpawners();
}