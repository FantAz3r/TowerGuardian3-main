using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerCardConfigContainer : MonoBehaviour
{
    [SerializeField] private CardData _cardData;
    [SerializeField] private List<CardConfig> _startCards;

    private Dictionary<CardType, ICardFactory> _factories;
    private List<ICardConfig> _selectedConfigs = new();
    public IReadOnlyList<ICardConfig> SelectedCardConfigs => _selectedConfigs;

    public event Action<ICardConfig> CardAdded, CardRemoved;
    public event Action Upgraded;

    private void Awake()
    {
        Player player = GetComponentInParent<Player>();

        _factories = new Dictionary<CardType, ICardFactory>()
        {
            { CardType.Weapon, new WeaponFactory(player) },
            {CardType.Ability, new AbilityFactory(player) }
        };
    }

    private void Start()
    {
        LoadPlayerCards();

        foreach (var card in _startCards)
        {
            card.SetBought(true);
        }
    }

    public void Add(ICardConfig config)
    {
        LoadCard(config);

        if (_selectedConfigs.Contains(config) == false)
        {
            AddCard(config);
        }

        if (config.HasPlayer || _startCards.Contains(config as CardConfig))
        {
            config.Upgrade();
            Upgraded?.Invoke();
        }

        UpdateCardSave(config);
    }

    public void Remove(ICardConfig config)
    {
        _selectedConfigs.Remove(config);
        CardRemoved?.Invoke(config);
    }

    private void Create(ICardConfig card)
    {
        if (_factories != null && _factories.TryGetValue(card.GetCardType(), out ICardFactory factory))
        {
            factory.Create(card);
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
                AddCard(card);
            }
        }
    }

    private void LoadCard(ICardConfig card)
    {
        if (YG2.saves.AllCards == null)
            return;

        CardSaveData cardData = YG2.saves.AllCards.Find(cardSave => cardSave.ID == card.ID);

        if (string.IsNullOrEmpty(cardData.ID) == false)
        {
            card.InitFromData(cardData);
        }
    }

    private void AddCard(ICardConfig card)
    {
        _selectedConfigs.Add(card);
        Create(card);
        CardAdded?.Invoke(card);
    }
}
