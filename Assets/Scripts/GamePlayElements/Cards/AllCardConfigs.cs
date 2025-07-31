using System.Collections.Generic;
using UnityEngine;

public class AllCardConfigs : MonoBehaviour
{
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    [SerializeField] private List<AbilityConfig> _abilityConfigs;
    [SerializeField] private List<BuffConfig> _buffConfigs;

    private PlayerConfigContainer _container;
    private List<ICardConfig> _configs = new List<ICardConfig>();

    public IEnumerable<ICardConfig> Configs => _configs;

    public void Init(PlayerConfigContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        _configs.AddRange(_weaponConfigs);
        _configs.AddRange(_abilityConfigs);
        _configs.AddRange(_buffConfigs);
    }

    public void Get(ICardConfig config)
    {
        if(_configs.Contains(config))
        {
            _container.Add(config);
        }
    }
}