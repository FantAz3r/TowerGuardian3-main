using UnityEngine;

public interface ISpawner
{
    SpawnerType GetSpawnerType();

    void Spawn(HealthConfig config, Vector3 position, int count);
    void Spawn(AudioClip clip, Vector3 position = default);
    void Spawn(Vector3 position, int damage, Color? textColor = null);
    void Spawn(HealthConfig config, Vector3 position);

    void EnableSpawn();
    void DisableSpawn();
    void DestroyPool();
}