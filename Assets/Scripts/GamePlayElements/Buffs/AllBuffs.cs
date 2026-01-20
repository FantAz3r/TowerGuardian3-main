using System.Collections.Generic;
using UnityEngine;

public class AllBuffs : MonoBehaviour
{
    private List<IBuff> _buffs = new List<IBuff>();
    private PlayerCardConfigContainer _container;

    private Health _health;
    private Mover _mover;
    private Inventory _inventory;
    private ResourceCollector _resourceCollector;
    private HealthRegeneration _healthRegen;

    public IEnumerable<IBuff> Buffs => _buffs;

    private void Awake()
    {
        _container = GetComponent<PlayerCardConfigContainer>();
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _healthRegen = GetComponent<HealthRegeneration>();

        _inventory = GetComponentInChildren<Inventory>();
        _resourceCollector = GetComponentInChildren<ResourceCollector>();

        CreateBuffs();
    }

    private void OnEnable()
    {
        _container.BuffAdded += Activate;
        _container.BuffRemoved += Deactivate;
        _container.Upgraded += Upgrade;
    }

    private void OnDisable()
    {
        _container.BuffAdded -= Activate;
        _container.BuffRemoved -= Deactivate;
        _container.Upgraded -= Upgrade;
    }

    private void CreateBuffs()
    {
        _buffs.Add(new MaxHealthBuff(_health));
        _buffs.Add(new SpeedBuff(_mover));
        _buffs.Add(new CollectRangeBuff(_resourceCollector));
        _buffs.Add(new RegenerationBuff(_healthRegen));
    }

    private void Activate(BuffConfig buff)
    {
        foreach (IBuff item in _buffs)
        {
            if (buff.BuffType == item.Type)
            {
                item.EnableBuff();
                item.UpdateBuff(buff.IncreaseValue);
            }
        }
    }

    private void Upgrade(ICardConfig card)
    {
        if(card is BuffConfig buff)
        {
            foreach (IBuff item in _buffs)
            {
                if (buff.BuffType == item.Type)
                {
                    item.UpdateBuff(buff.IncreaseValue);
                }
            }
        }
    }

    private void Deactivate(BuffConfig buff)
    {
        foreach (IBuff item in _buffs)
        {
            if (buff.BuffType == item.Type)
            {
                item.UpdateBuff(0);
            }
        }
    }
}
