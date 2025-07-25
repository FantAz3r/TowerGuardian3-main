using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelector
{
    private List<ICard> _allCards = new List<ICard>();
    private float _weaponCance = 0.4f;
    private float _buffCance = 0.35f;
    private float _abilityCance = 0.25f;

    private int _cardsCount = 3;
    private PlayerCardContainer _playerCards;

    private struct Card
    {
        public CardType Type;
        public float Chance;

        public Card(CardType type, float chance)
        {
            Type = type;
            Chance = chance;
        }
    }

    public List<ICard> GetCards()
    {
        List<Card> cards = new List<Card>()
        {
            new Card(CardType.WeaponSetter, _weaponCance),
            new Card(CardType.Buff, _buffCance),
            new Card(CardType.Ability, _abilityCance)
        };

        cards = NormalizeChances(cards);

        List<ICard> selectedCards = new List<ICard>();
        int attempts = 0;

        while (selectedCards.Count < _cardsCount && attempts < 50)
        {
            attempts++;

            CardType selectedType = SelectCardTypeByChance(cards);

            List<ICard> filteredCards = FilterCards(_allCards, selectedType);

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

    private CardType SelectCardTypeByChance(List<Card> cards)
    {
        float rand = Random.value;
        float cumulative = 0f;

        foreach (var card in cards)
        {
            cumulative += card.Chance;
            if (rand <= cumulative)
                return card.Type;
        }

        return cards[0].Type;
    }

    private List<ICard> FilterCards(List<ICard> allCards, CardType selectedType)
    {
        return allCards.FindAll(card => card.Type == selectedType && _playerCards.SelectedCards.Contains(card) == false);
    }

    private List<Card> NormalizeChances(List<Card> cards)
    {
        float totalChance = 0f;
        foreach (var card in cards)
            totalChance += card.Chance;

        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            cards[i] = new Card(card.Type, card.Chance / totalChance);
        }

        return cards;
    }
}