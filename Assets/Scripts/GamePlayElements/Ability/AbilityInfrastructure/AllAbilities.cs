using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllAbilities : MonoBehaviour
{
    private List<Ability> _abilities;
    private PlayerCardConfigContainer _container;

    public event Action<AbilityConfig, IAbility> AbilityActivated;

    private void Awake()
    {
        _container = GetComponentInParent<PlayerCardConfigContainer>();
        _abilities = GetComponents<Ability>().ToList();
    }

    private void OnEnable()
    {
        _container.AbilityAdded += Activate;
    }

    private void OnDisable()
    {
        _container.AbilityAdded -= Activate;
    }

    private void Activate(AbilityConfig ability)
    {
        foreach (IAbility item in _abilities)
        {
            if (ability.Type == item.AbilityType)
            {
                item.Enable();
                AbilityActivated?.Invoke(ability, item);
            }
        }
    }
}
