using System;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Projectiles;

namespace TowerGuardian.Scripts.StaticData.Structs
{
    [Serializable]

    public struct ProjectileInfo
    {
        public Projectile Prefab;
        public ProjectileType Type;
    }
}