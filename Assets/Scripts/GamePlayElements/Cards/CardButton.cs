using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CardButton : MonoBehaviour
{
    private ICardConfig _card;
    private Button _button;
    private PlayerCardConfigContainer _playerCards;

    public event Action Selected;

    public void Init(PlayerCardConfigContainer playerCards)
    {
        _playerCards = playerCards;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
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
    }

    public void OnClick()
    {
        _playerCards.Add(_card);
        Selected?.Invoke();
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