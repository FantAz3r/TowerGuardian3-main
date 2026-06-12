using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Projectiles;
using TowerGuardian.Scripts.StaticData.Configs.EntityConfigs;
using UnityEngine;

namespace TowerGuardian.Scripts.Spawners
{
    public abstract class BaseSpawner : ISpawner
    {
        private bool _canSpawn = true;

        public bool CanSpawn => _canSpawn;

        public abstract SpawnerType GetSpawnerType();

        public virtual void Spawn(HealthConfig config, Vector3 position, int count)
        {
        }

        public virtual void Spawn(AudioClip clip, Vector3 position = default)
        {
        }

        public virtual void Spawn(Vector3 position, int damage, Color? textColor = null)
        {
        }

        public virtual void Spawn(EffectType type, Vector3 position, Transform parent = null)
        {
        }

        public virtual Projectile Spawn(ProjectileType projectileType, Vector3 position, Transform parent = null)
        {
            return null;
        }

        public virtual void EnableSpawn()
        {
            _canSpawn = true;
        }

        public virtual void ClearObjects()
        {
        }

        public virtual void DisableSpawn()
        {
            _canSpawn = false;
        }

        public abstract void DestroyPool();
    }
}
