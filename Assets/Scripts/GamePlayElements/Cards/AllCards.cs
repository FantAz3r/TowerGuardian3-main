using System.Collections.Generic;
using UnityEngine;

public class AllCards : MonoBehaviour
{
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    [SerializeField] private List<AbilityConfig> _abilityConfigs;
    [SerializeField] private List<BuffConfig> _buffConfigs;

    private PlayerCardContainer _container;
    private List<IConfig> _configs;
    private List<ICard> _cards;

    public IEnumerable<ICard> Cards => _cards;

    public void Init(PlayerCardContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        _configs = new List<IConfig>();
        _cards = new List<ICard>();

        _configs.AddRange(_weaponConfigs);
        _configs.AddRange(_abilityConfigs);
        _configs.AddRange(_buffConfigs);

        foreach (var config in _configs)
        {
            ICard card = Create(config);

            if (card != null)
            {
                _cards.Add(card);
            }
            else
            {
                Debug.LogWarning($"Неизвестный тип конфига: {config.GetType()}");
            }
        }
    }

    public void Get(IConfig config)
    {
        foreach(var card in _cards)
        {
            if(config == card.Config)
            {
                _container.Add(card);
            }
        }
    }

    private ICard Create(IConfig config)
    {
        switch (config)
        {
            case WeaponConfig weaponConfig:
                return new WeaponCard(weaponConfig);

            case AbilityConfig abilityConfig:
                return new AbilityCard(abilityConfig);

            case BuffConfig buffConfig:
                return new BuffCard(buffConfig);

            default:
                return null;
        }
    }
}