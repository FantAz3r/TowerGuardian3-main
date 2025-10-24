using System.Collections.Generic;
using UnityEngine;
using YG;

public class AllCardConfigs : MonoBehaviour
{
    [SerializeField] private List<WeaponConfig> _weaponConfigs;
    [SerializeField] private List<AbilityConfig> _abilityConfigs;
    [SerializeField] private List<BuffConfig> _buffConfigs;

    private PlayerCardConfigContainer _container;
    private List<ICardConfig> _configs = new List<ICardConfig>();

    public IReadOnlyList<ICardConfig> Configs => _configs;

    public void Init(PlayerCardConfigContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        _configs.AddRange(_weaponConfigs);
        _configs.AddRange(_abilityConfigs);
        _configs.AddRange(_buffConfigs);

        if(YG2.saves.Cards == null)
            return;

        if (YG2.saves.Cards.Count <= 0)
            return;

        LoadCards();
    }

    private void Start()
    {
        if (YG2.saves.PlayerCards == null)
            return;

        if (YG2.saves.PlayerCards.Count <= 0)
            return;

        LoadPlayerCards();
    }

    public void Add(ICardConfig config)
    {
        Debug.Log(config.Name + " Added");
        _configs.Add(config);
    }

    public void Get(ICardConfig config)
    {
        if(_configs.Contains(config))
        {
            _container.Add(config);
        }
    }

    public void SaveCards()
    {
        List<CardSaveData> cards = new List<CardSaveData>();

        foreach(var card in _configs)
        {
            cards.Add(card.CreateSaveData());
        }

        YG2.saves.Cards = cards;
        YG2.SaveProgress();
    }

    private void LoadCards()
    {
        for (int i = 0; i < _configs.Count; i++)
        {
            _configs[i].InitFromData(YG2.saves.Cards[i]);
        }
    }

    private void LoadPlayerCards()
    {
        for (int i = 0; i < _configs.Count; i++)
        {
            if (YG2.saves.PlayerCards.Contains(_configs[i].Name))
            {
                _container.Add(_configs[i]);
            }
        }
    }
}

