using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardButton : MonoBehaviour
{
    private ICard _card;
    private Button _button;
    private CardFactory _factory;

    public event Action Selected;

    public void Init(CardFactory factory)
    {
        _factory = factory;
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

    public void Select()
    {
        _factory.Create(_card.Config);
        Selected?.Invoke();
    }
}