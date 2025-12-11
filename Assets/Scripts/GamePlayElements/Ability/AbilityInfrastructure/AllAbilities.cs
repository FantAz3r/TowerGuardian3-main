using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllAbilities : MonoBehaviour
{
    private List<Ability> _abilities;
    private PlayerCardConfigContainer _container;

    public event Action<AbilityConfig, IAbility> AbilityActivated;
    public event Action<AbilityConfig, IAbility> AbilityRemoved;
    public event Action<float> DialedDamage;

    private void Awake()
    {
        _container = GetComponentInParent<PlayerCardConfigContainer>();
        _abilities = GetComponents<Ability>().ToList();

        _container.AbilityAdded += OnActivate;
        _container.Upgraded += OnUpgrade;
        _container.AbilityRemoved += OnRemove;
    }

    private void OnDestroy()
    {
        _container.AbilityAdded -= OnActivate;
        _container.Upgraded -= OnUpgrade;
        _container.AbilityRemoved -= OnRemove;

        foreach (IAbility item in _abilities)
        {
            if (item is IDamageAbility damageAbility)
            {
                damageAbility.DialedDamage -= OnHit;
            }
        }
    }

    private void OnActivate(AbilityConfig ability)
    {

        foreach (IAbility item in _abilities)
        {
            if (ability.Type == item.AbilityType)
            {
                item.Enable();
                AbilityActivated?.Invoke(ability, item);

                if (item is IDamageAbility damageAbility)
                {
                    damageAbility.DialedDamage += OnHit;
                }
            }
        }
    }

    private void OnUpgrade(ICardConfig card)
    {
        Debug.Log("Shurikers Upgraded 2");

        if (card is AbilityConfig ability)
        {
            foreach (IAbility item in _abilities)
            {
                if (ability.Type == item.AbilityType)
                {
                    item.Upgrade();
                }
            }
        }
    }

    private void OnRemove(ICardConfig card)
    {
        if (card is AbilityConfig ability)
        {
            foreach (IAbility item in _abilities)
            {
                if (ability.Type == item.AbilityType)
                {
                    item.Remove();
                    AbilityRemoved?.Invoke(ability, item);

                    if (item is IDamageAbility damageAbility)
                    {
                        damageAbility.DialedDamage -= OnHit;
                    }
                }
            }
        }
    }

    private void OnHit(float damage)
    {
        DialedDamage?.Invoke(damage);
    }
}
