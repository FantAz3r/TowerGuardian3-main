using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelector
{
    private AllCardConfigs _allConfigs;
    private int _cardsCount;
    private PlayerConfigContainer _playerCards;

    public CardSelector(AllCardConfigs configs, PlayerConfigContainer playerCards, int cardsCount = 3)
    {
        _allConfigs = configs;
        _playerCards = playerCards;
        _cardsCount = cardsCount;
    }

    public int CardsCount => GetRemainingCardsCount();


    public IEnumerable<ICardConfig> GetCards()
    {
        List<ICardConfig> selectedCards = new List<ICardConfig>();
        int attempts = 0;
        int maxAttempts = 100;

        while (selectedCards.Count < GetRemainingCardsCount() && attempts < maxAttempts)
        {
            attempts++;

            List<ICardConfig> filteredCards = FilterCards(_allConfigs.Configs.ToList());
            ICardConfig chosenCard = SelectCardByChance(filteredCards);

            if (selectedCards.Contains(chosenCard) == false)
            {
                selectedCards.Add(chosenCard);
            }
        }

        return selectedCards;
    }

    public int GetRemainingCardsCount()
    {
        int selectedCount = _playerCards.SelectedConfigs.Count();
        int totalCards = _allConfigs.Configs.Count();
        int cardsToView = Mathf.Min(_cardsCount, totalCards - selectedCount);
        return cardsToView;
    }

    private ICardConfig SelectCardByChance(IEnumerable<ICardConfig> allCards)
    {
        float totalChance = allCards.Sum(card => card.ChanceToView);

        float rand = Random.value * totalChance;
        float cumulative = 0f;

        foreach (var card in allCards)
        {
            cumulative += card.ChanceToView;
            if (rand <= cumulative)
                return card;
        }

        return null;
    }

    private List<ICardConfig> FilterCards(List<ICardConfig> allCards)
    {
        return allCards.FindAll(card => _playerCards.SelectedConfigs.Contains(card) == false);
    }
}

