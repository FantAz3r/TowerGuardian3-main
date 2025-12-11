using UnityEngine;

public interface ISpawnerService : IService
{
    void RegisterSpawner(ISpawner spawner);

    void SendReqest(SpawnerType spawnerType, HealthConfig config, Vector3 position, int count = 1);

    void EnableSpawner(SpawnerType spawnerType);

    void DisableSpawn(SpawnerType spawnerType);

    void DestroySpawners();
}