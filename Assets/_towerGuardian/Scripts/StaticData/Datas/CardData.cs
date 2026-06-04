using System.Collections.Generic;
using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Datas/CardData")]

    public class CardData : ScriptableObject
    {
        public List<WeaponConfig> Weapons;
        public List<AbilityConfig> Abilities;
        public List<BuffConfig> BuffConfigs;

        public List<ICardConfig> GetConfigs()
        {
            List<ICardConfig> configs = new List<ICardConfig>();
            configs.AddRange(Weapons);
            configs.AddRange(Abilities);
            configs.AddRange(BuffConfigs);
            return configs;
        }
    }
}