using System.Collections.Generic;
using UnityEngine;

public class CardSelector
{
    private const int MaxWeaponCards = 3;
    private const int MaxAbilityCards = 3;

    private CardData _cardData;
    private int _cardsPerSelect;
    private int _maxLevel = 100;

    private List<ICardConfig> _currentCards = new();

    public CardSelector(int cardsCount = 3)
    {
        _cardData = Resources.Load<CardData>(GameConstants.CardData);
        _cardsPerSelect = cardsCount;
    }

    public IEnumerable<ICardConfig>GetCards()
    {
        if(_currentCards != null && _currentCards.Count >0)
        {
            return _currentCards;
        }

        List<ICardConfig> startFiltered = FilterCards(_cardData.GetConfigs());

        if (startFiltered.Count == 0)
            return new List<ICardConfig>();

        List<ICardConfig> baseFiltered = Utils.Shuffle(startFiltered);

        int remainingCards = Mathf.Min(_cardsPerSelect, baseFiltered.Count);

        List<ICardConfig> selectedCards = new List<ICardConfig>(remainingCards);
        List<ICardConfig> available = new List<ICardConfig>(baseFiltered);

        while (selectedCards.Count < remainingCards && available.Count > 0)
        {
            var chosen = SelectCardByChance(available);
            if (chosen == null)
                break;

            if(selectedCards.Contains(chosen) == false)
            {
                selectedCards.Add(chosen);
                available.Remove(chosen);
            }
        }

        return selectedCards;
    }

    public void SaveCurrentCards(List<ICardConfig> currentCards)
    {
        if(currentCards == null)
        {
            _currentCards.Clear();
            return;
        }
       
        _currentCards = currentCards;
    }

    private ICardConfig SelectCardByChance(IList<ICardConfig> list)
    {
        if (list == null || list.Count == 0)
            return null;

        float totalChance = 0f;

        for (int i = 0; i < list.Count; i++)
        {
            var c = list[i];
            float w = c.ChanceToView;
            if (w < 0f) w = 0f;
            totalChance += w;
        }

        if (totalChance <= 0f)
        {
            int idx = Random.Range(0, list.Count);
            return list[idx];
        }

        float rand = Random.Range(0f, totalChance);
        float cumulative = 0f;

        for (int i = 0; i < list.Count; i++)
        {
            cumulative += Mathf.Max(0f, list[i].ChanceToView);
            if (rand < cumulative)
                return list[i];
        }

        return list[list.Count - 1];
    }

    private List<ICardConfig> FilterCards(List<ICardConfig> allCards)
    {
        int weaponCount = 0;
        int abilityCount = 0;

        foreach (var card in allCards)
        {
            if (card is WeaponConfig && card.HasPlayer)
                weaponCount++;

            if (card is AbilityConfig && card.HasPlayer)
                abilityCount++;
        }

        var filteredCards = new List<ICardConfig>();

        foreach (var card in allCards)
        {
            if (card.Level >= _maxLevel)
                continue;

            if (card is WeaponConfig)
            {
                if (weaponCount < MaxWeaponCards)
                {
                    if (card.IsBought)
                        filteredCards.Add(card);
                }
                else
                {
                    if (card.HasPlayer)
                        filteredCards.Add(card);
                }
            }
            else if (card is AbilityConfig)
            {
                if (abilityCount < MaxAbilityCards)
                {
                    if (card.IsBought)
                        filteredCards.Add(card);
                }
                else
                {
                    if (card.HasPlayer)
                        filteredCards.Add(card);
                }
            }
            else
            {
                filteredCards.Add(card);
            }
        }

        return filteredCards;
    }

}