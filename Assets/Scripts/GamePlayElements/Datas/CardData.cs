using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Datas/CardData")]

public class CardData : ScriptableObject
{
    public List<WeaponConfig> Weapons = new();
    public List<AbilityConfig> Abilities = new();
    public List<BuffConfig> BuffConfigs = new();

    public List<ICardConfig> GetConfigs()
    {
        List<ICardConfig> configs = new List<ICardConfig>();
        configs.AddRange(Weapons);
        configs.AddRange(Abilities);
        configs.AddRange(BuffConfigs);
        return configs;
    }
}
