using UnityEngine;

public interface ISpawnerService : IService
{
    void RegisterSpawner(ISpawner spawner);

    void SendItemReqest(HealthConfig config, Vector3 position, int count = 1);
    void SendSoundReqest(AudioClip clip, Vector3 position = default);
    void SendTextReqest(Vector3 position, int damage, Color? textColor = null);
    void SendEffectReqest(HealthConfig config, Vector3 position);

    void EnableSpawner(SpawnerType spawnerType);
    void DisableSpawn(SpawnerType spawnerType);
    void DestroySpawners();
}