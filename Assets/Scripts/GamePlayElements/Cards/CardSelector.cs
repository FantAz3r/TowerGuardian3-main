using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelector
{
    private AllCardConfigs _allConfigs;
    private int _cardsCount;
    private PlayerCardConfigContainer _playerCards;

    public CardSelector(AllCardConfigs configs, PlayerCardConfigContainer playerCards, int cardsCount = 3)
    {
        _allConfigs = configs;
        _playerCards = playerCards;
        _cardsCount = cardsCount;
        //foreach (var card in _allConfigs.Configs.ToList())
        //{
        //    Debug.Log(card.CardType);
        //}
    }

    public IEnumerable<ICardConfig> GetCards()
    {
        List<ICardConfig> selectedCards = new List<ICardConfig>();
        int attempts = 0;
        int maxAttempts = 100;

        List<ICardConfig> baseFiltered = FilterCards(_allConfigs.Configs.ToList());

        while (selectedCards.Count < GetRemainingCardsCount() && attempts < maxAttempts)
        {
            attempts++;

            List<ICardConfig> available = baseFiltered.Where(card => selectedCards.Contains(card) == false).ToList();

            if (available.Count == 0)
                break;

            ICardConfig chosenCard = SelectCardByChance(available);

            if (chosenCard != null && selectedCards.Contains(chosenCard) == false)
            {
                selectedCards.Add(chosenCard);
            }
        }

        return selectedCards;
    }

    public int GetRemainingCardsCount()
    {
        int selectedCount = _playerCards.SelectedCardConfigs.Count();
        int totalCards = _allConfigs.Configs.Count();
        int cardsToView = Mathf.Min(_cardsCount, totalCards - selectedCount);
        return cardsToView;
    }

    private ICardConfig SelectCardByChance(IEnumerable<ICardConfig> allCards)
    {
        var list = allCards.ToList();
        if (list.Count == 0)
            return null;

        float totalChance = list.Sum(card => card.ChanceToView);

        if (totalChance <= 0f)
            return list[Random.Range(0, list.Count)];

        float rand = Random.value * totalChance;
        float cumulative = 0f;

        foreach (var card in list)
        {
            cumulative += card.ChanceToView;
            if (rand <= cumulative)
                return card;
        }

        return null;
    }

    private List<ICardConfig> FilterCards(List<ICardConfig> allCards)
    {
        //if (_playerCards.FullAbilities)
        //{
        //    allCards.RemoveAll(card => card.CardType == CardType.Ability);
        //}

        return allCards.FindAll(card => _playerCards.SelectedCardConfigs.Contains(card) == false);
    }
}
