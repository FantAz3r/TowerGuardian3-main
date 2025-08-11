using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardConfigContainer : MonoBehaviour
{
    [SerializeField] private int _maxAbilitiesCount = 4;
    [SerializeField] private int _maxWeaponsCount = 3;
    private List<ICardConfig> _selectedConfigs = new List<ICardConfig>();
    public IEnumerable<ICardConfig> SelectedCardConfigs => _selectedConfigs;
    public bool FullAbilities => _maxAbilitiesCount <= 0;

    public event Action<BuffConfig> BuffAdded;
    public event Action<AbilityConfig> AbilityAdded;
    public event Action<WeaponConfig> WeaponAdded;

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
            _maxAbilitiesCount--;
            AbilityAdded?.Invoke(ability);
        }

        if(config is WeaponConfig weapon)
        {
            _maxWeaponsCount--;
            WeaponAdded?.Invoke(weapon);
        }
    }
}
