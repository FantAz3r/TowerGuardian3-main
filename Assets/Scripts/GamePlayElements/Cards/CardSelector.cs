using System.Collections.Generic;
using UnityEngine;

public class CardSelector
{
    private CardData _cardData;
    private int _cardsPerSelect;
    private int _maxLevel = 100;

    public CardSelector(CardData cardData, int cardsCount = 3)
    {
        _cardData = cardData;
        _cardsPerSelect = cardsCount;
    }

    public IEnumerable<ICardConfig>GetCards()
    {
        var baseFiltered = FilterCards(_cardData.GetConfigs());

        Debug.Log(_cardData.GetConfigs().Count);
        Debug.Log(baseFiltered.Count);

        if (baseFiltered.Count == 0)
            return new List<ICardConfig>();

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
        return allCards.FindAll(card => card.Level < _maxLevel && card.IsBought);
    }
}