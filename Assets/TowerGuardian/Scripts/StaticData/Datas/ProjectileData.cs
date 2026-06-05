using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Projectiles;
using TowerGuardian.Scripts.StaticData.Structs;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
{
    [CreateAssetMenu(fileName = "Datas", menuName = "Datas/Projectile")]

    public class ProjectileData : ScriptableObject
    {
        public List<ProjectileInfo> ProjectileInfos = new List<ProjectileInfo>();

        public Projectile GetProJectile(ProjectileType type)
        {
            foreach (var item in ProjectileInfos)
            {
                if (item.Type == type)
                    return item.Prefab;
            }

            return default;
        }
    }
}