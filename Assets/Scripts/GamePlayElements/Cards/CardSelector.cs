using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelector
{
    private AllCards _allCards;
    private int _cardsCount = 3;
    private PlayerCardContainer _playerCards;

    public CardSelector(AllCards factory, PlayerCardContainer playerCards)
    {
        _allCards = factory;
        _playerCards = playerCards;
    }

    public IEnumerable<ICard> GetCards()
    {
        List<ICard> selectedCards = new List<ICard>();
        int attempts = 0;

        while (selectedCards.Count < _cardsCount && attempts < 50)
        {
            attempts++;

            CardType selectedType = SelectTypeByChance(_allCards.Cards);

            List<ICard> filteredCards = FilterCards(_allCards.Cards.ToList(), selectedType);

            if (filteredCards.Count == 0)
                continue;

            ICard chosenCard = filteredCards[Random.Range(0, filteredCards.Count)];

            if (selectedCards.Contains(chosenCard) == false)
            {
                selectedCards.Add(chosenCard);
            }
        }

        return selectedCards;
    }

    private CardType SelectTypeByChance(IEnumerable<ICard> allCards)
    {
        float totalChance = 0f;

        foreach (var card in allCards)
        {
            totalChance += card.ChanseToView;
        }

        float rand = Random.value * totalChance;
        float cumulative = 0f;

        foreach (var card in allCards)
        {
            cumulative += card.ChanseToView;
            if (rand <= cumulative)
                return card.Type;
        }

        return allCards.First().Type;
    }

    private List<ICard> FilterCards(List<ICard> allCards, CardType selectedType)
    {
        return allCards.FindAll(card => card.Type == selectedType && _playerCards.SelectedCards.Contains(card) == false);
    }
}