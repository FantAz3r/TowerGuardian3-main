using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Datas/CardData")]

public class CardData : ScriptableObject
{
    public List<WeaponConfig> _weapons;
    public List<AbilityConfig> _abilities;
    public List<BuffConfig> _buffConfigs;

    public List<ICardConfig> GetConfigs()
    {
        List<ICardConfig> configs = new List<ICardConfig>();
        configs.AddRange(_weapons);
        configs.AddRange(_abilities);
        configs.AddRange(_buffConfigs);
        return configs;
    }
}
