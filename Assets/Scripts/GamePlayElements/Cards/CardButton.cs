using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardButton : MonoBehaviour
{
    private ICard _card;
    private Button _button;
    private AllCards _cards;
    private Dictionary<CardType, ICardFactory> _factories;

    public event Action Selected;

    public void Init(AllCards cards, List<ICardFactory> factories)
    {
        _cards = cards;
        _factories = new Dictionary<CardType, ICardFactory>();

        foreach (var factory in factories)
        {
            if (factory is ICardFactory cardFactory)
            {
                _factories[cardFactory.Type] = cardFactory;
            }
        }

        _button.onClick.AddListener(Select);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Select);
    }

    public void SetCard(ICard card)
    {
        _card = card;
    }

    public void Select()
    {
        _cards.Get(_card.Config);
        Selected?.Invoke();
        ActivateCard(_card);
    }

    private void ActivateCard(ICard card)
    {
        if (_factories != null && _factories.TryGetValue(card.Type, out ICardFactory factory))
        {
            factory.ActivateCard(card.Config);
        }
    }
}