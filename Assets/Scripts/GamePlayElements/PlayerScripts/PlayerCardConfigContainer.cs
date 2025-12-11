using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerCardConfigContainer : MonoBehaviour
{
    [SerializeField] private CardData _cardData;
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

        YG2.saves.AllCards.RemoveAll(savedCard => savedCard.Name == card.Name);
        YG2.saves.AllCards.Add(new CardSaveData(card.Level, card.Name, card.IsBought, true));
        YG2.SaveProgress();
    }

    private void LoadPlayerCards()
    {
        if (YG2.saves.AllCards == null)
            return;

        foreach (var card in _cardData.GetConfigs())
        {
            CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.Name == card.Name);
            card.InitFromData(cardData);

            if (card.HasPlayer)
            {
                Add(card);
            }
        }
    }

    private void LoadCard(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            return;

        CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.Name == card.Name);

        if( string.IsNullOrEmpty(cardData.Name) == false)
        {
            card.InitFromData(cardData);
        }
    }
}
