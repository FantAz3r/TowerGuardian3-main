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

    public IEnumerable<IBuff> Buffs => _buffs;

    private void Awake()
    {
        _container = GetComponent<PlayerCardConfigContainer>();
        _inventory = GetComponentInChildren<Inventory>();
        _resourceCollector = GetComponentInChildren<ResourceCollector>();
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        CreateBuffs();
    }

    private void OnEnable()
    {
        _container.BuffAdded += Activate;
    }

    private void OnDisable()
    {
        _container.BuffAdded -= Activate;
    }

    private void CreateBuffs()
    {
        _buffs.Add(new MaxHealthBuff(_health));
        _buffs.Add(new SpeedBuff(_mover));
    }

    private void Activate(BuffConfig buff)
    {
        foreach (IBuff item in _buffs)
        {
            if (buff.Type == item.Type)
            {
                item.ApplyBuff(buff.IncreaceValue);
            }
        }
    }
}
