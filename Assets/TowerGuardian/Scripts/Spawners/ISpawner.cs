using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Projectiles;
using TowerGuardian.Scripts.StaticData.Configs.EntityConfigs;
using UnityEngine;

namespace TowerGuardian.Scripts.Spawners
{
    public interface ISpawner
    {
        SpawnerType GetSpawnerType();

        void Spawn(HealthConfig config, Vector3 position, int count);
        void Spawn(AudioClip clip, Vector3 position = default);
        void Spawn(Vector3 position, int damage, Color? textColor = null);
        void Spawn(EffectType type, Vector3 position, Transform parent = null);
        Projectile Spawn(ProjectileType projectileType, Vector3 position, Transform parent = null);
        void EnableSpawn();
        void DisableSpawn();
        void ClearObjects();
        void DestroyPool();
    }
}