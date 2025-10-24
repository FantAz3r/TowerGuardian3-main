using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardButton : MonoBehaviour
{
    private ICardConfig _card;
    private Button _button;
    private AllCardConfigs _cards;
    private Dictionary<CardType, ICardFactory> _factories;

    public event Action Selected;

    public void Init(AllCardConfigs cards, List<ICardFactory> factories)
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
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        Hide();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void SetCard(ICardConfig card)
    {
        _card = card;
        Show();
    }

    public void OnClick()
    {
        _card.Upgrade();
        _cards.Get(_card);
        Selected?.Invoke();
        ActivateCard(_card);
        _cards.SaveCards();
        Hide();
    }

    private void ActivateCard(ICardConfig card)
    {
        if (_factories != null && _factories.TryGetValue(card.GetCardType(), out ICardFactory factory))
        {
            factory.ActivateCard(card);
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}