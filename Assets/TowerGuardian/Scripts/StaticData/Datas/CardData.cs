using System.Collections.Generic;
using System.ComponentModel;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Datas
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

        public BuffConfig Get(BuffType buffType)
        {
            foreach (var config in BuffConfigs)
            {
                if (buffType == config.BuffType)
                {
                    return config;
                }
            }

            throw new InvalidEnumArgumentException();
        }
    }
}