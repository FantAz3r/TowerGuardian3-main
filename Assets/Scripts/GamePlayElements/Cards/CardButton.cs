using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardButton : MonoBehaviour
{
    private ICardConfig _card;
    private Button _button;
    private AllConfigs _cards;
    private Dictionary<CardType, ICardFactory> _factories;

    public event Action Selected;

    public void Init(AllConfigs cards, List<ICardFactory> factories)
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
        Show();
        _card = card;
    }

    public void OnClick()
    {
        _cards.Get(_card);
        Selected?.Invoke();
        ActivateCard(_card);
        Hide();
    }

    private void ActivateCard(ICardConfig card)
    {
        if (_factories != null && _factories.TryGetValue(card.Type, out ICardFactory factory))
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