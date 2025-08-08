using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardConfigContainer : MonoBehaviour
{
    private List<ICardConfig> _selectedConfigs = new List<ICardConfig>();
    public IEnumerable<ICardConfig> SelectedCardConfigs => _selectedConfigs;

    public event Action<BuffConfig> BuffAdded;
    public event Action<AbilityConfig> AbilityAdded;

    public void Add(ICardConfig config)
    {
        _selectedConfigs.Add(config);

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
    }
}
