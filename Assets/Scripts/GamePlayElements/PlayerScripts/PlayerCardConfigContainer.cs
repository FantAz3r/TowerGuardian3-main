using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerCardConfigContainer : MonoBehaviour
{
    private List<ICardConfig> _selectedConfigs = new();
    public IEnumerable<ICardConfig> SelectedCardConfigs => _selectedConfigs;

    public event Action<BuffConfig> BuffAdded;
    public event Action<AbilityConfig> AbilityAdded;
    public event Action<WeaponConfig> WeaponAdded;

    public void Add(ICardConfig config)
    { 
        if (_selectedConfigs.Contains(config))
           return;

        _selectedConfigs.Add(config);
        SavePlayerCard(config);
        Define(config);
    }

    public void Define(ICardConfig config)
    {
        if (config is BuffConfig buff)
        {
            BuffAdded?.Invoke(buff);
        }

        if (config is AbilityConfig ability)
        {
            AbilityAdded?.Invoke(ability);
        }

        if(config is WeaponConfig weapon)
        {
            WeaponAdded?.Invoke(weapon);
        }
    }

    public void SavePlayerCard(ICardConfig card)
    {
        if (YG2.saves.PlayerCards == null)
        {
            YG2.saves.PlayerCards = new List<string>();
        }

        YG2.saves.PlayerCards.Add(card.Name);
        YG2.SaveProgress();
    }

    public void Remove(ICardConfig card)
    {
        YG2.saves.PlayerCards.Remove(card.Name);
    }
}
