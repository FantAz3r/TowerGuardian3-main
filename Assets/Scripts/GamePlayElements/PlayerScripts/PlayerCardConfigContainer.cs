using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerCardConfigContainer : MonoBehaviour
{
    [SerializeField] private CardData _cardData;
    [SerializeField] private List<CardConfig> _startCards;

    private List<ICardConfig> _selectedConfigs = new();
    public IReadOnlyList<ICardConfig> SelectedCardConfigs => _selectedConfigs;

    public event Action<BuffConfig> BuffAdded;
    public event Action<AbilityConfig> AbilityAdded;
    public event Action<WeaponConfig> WeaponAdded;
    public event Action<AbilityConfig> AbilityRemoved;
    public event Action<WeaponConfig> WeaponRemoved;
    public event Action<BuffConfig> BuffRemoved;
    public event Action<ICardConfig> Upgraded;
    public event Action Added;

    private void Start()
    {
        foreach (var card in _startCards)
        {
            card.SetBought(true);
        }

        LoadPlayerCards();
    }

    public void Add(ICardConfig config)
    {
        LoadCard(config);

        if (_selectedConfigs.Contains(config) == false)
        {
            _selectedConfigs.Add(config);
            Define(config);
        }

        if (config.HasPlayer)
        {
            config.Upgrade();
            Upgraded.Invoke(config);
        }

        UpdateCardSave(config);
    }

    public void Remove(ICardConfig config)
    {
        _selectedConfigs.Remove(config);

        if (config is AbilityConfig ability)
            AbilityRemoved?.Invoke(ability);

        if (config is WeaponConfig weapon)
            WeaponRemoved?.Invoke(weapon);

        if (config is BuffConfig buff)
            BuffRemoved?.Invoke(buff);
    }

    private void Define(ICardConfig config)
    {
        Added?.Invoke();

        if (config is BuffConfig buff)
        {
            BuffAdded?.Invoke(buff);
        }

        if (config is AbilityConfig ability)
        {
            AbilityAdded?.Invoke(ability);
        }

        if (config is WeaponConfig weapon)
        {
            WeaponAdded?.Invoke(weapon);
        }
    }

    private void UpdateCardSave(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            YG2.saves.AllCards = new();

        YG2.saves.AllCards.RemoveAll(savedCard => savedCard.ID == card.ID);
        YG2.saves.AllCards.Add(new CardSaveData(card.Level, card.ID, card.IsBought, true));
        YG2.SaveProgress();
    }

    private void LoadPlayerCards()
    {
        if (YG2.saves.AllCards == null)
            return;

        foreach (var card in _cardData.GetConfigs())
        {
            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);
            card.InitFromData(cardData);

            if (card.HasPlayer)
            {
                LoadCard(card);
                _selectedConfigs.Add(card);
                Define(card);
            }
        }
    }

    private void LoadCard(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            return;

        CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);

        if( string.IsNullOrEmpty(cardData.ID) == false)
        {
            card.InitFromData(cardData);
        }
    }
}
