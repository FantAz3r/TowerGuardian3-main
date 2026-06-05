using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Projectiles;
using TowerGuardian.Scripts.Spawners;
using TowerGuardian.Scripts.StaticData.Configs.EntityConfigs;
using UnityEngine;

namespace TowerGuardian.Scripts.Infrastructure.Servises.Interfaces
{
    public interface ISpawnerService : IService
    {
        void RegisterSpawner(ISpawner spawner);

        void SendItemReqest(HealthConfig config, Vector3 position, int count = 1);
        void SendSoundReqest(AudioClip clip, Vector3 position = default);
        void SendTextReqest(Vector3 position, int damage, Color? textColor = null);
        void SendEffectReqest(EffectType type, Vector3 position, Transform parent = null);
        Projectile SendProjectileRequest(ProjectileType type, Vector3 position, Transform parent = null);

        void EnableSpawner(SpawnerType spawnerType);
        void DisableSpawn(SpawnerType spawnerType);
        void ClearObjects(SpawnerType spawnerType);
        void DestroySpawners();
    }
}