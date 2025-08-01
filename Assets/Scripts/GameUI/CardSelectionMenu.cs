using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionMenu : MonoBehaviour
{
    private List<CardButton> _cardsButtons;
    private PlayerExperience _playerExperience;
    private ITimeService _timeService;
    private bool _isMenuOpen = false;
    private CardSelector _selector;

    public event Action<int, ICardConfig> CardLoaded;

    public void Init(ITimeService timeService, PlayerExperience playerExperience, CardSelector selector,List<CardButton> cardsButtons)
    {
        _timeService = timeService;
        _playerExperience = playerExperience;
        _selector = selector;
        _cardsButtons = cardsButtons;

        _playerExperience.OnLevelUp += Open;

        foreach (var button in _cardsButtons)
        {
            button.Selected += Close;
            GridLayoutGroup panel = GetComponentInChildren<GridLayoutGroup>();
            button.transform.SetParent(panel.transform);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _playerExperience.OnLevelUp -= Open;

        foreach (var button in _cardsButtons)
        {
            button.Selected -= Close;
        }
    }

    public void Open(int level)
    {
        List<ICardConfig> cards = _selector.GetCards().ToList();

        if (cards.Count > 0)
        {
            SetMenuOpen(true);
            _timeService.Pause();
            ShowCards(cards);
        }
    }

    public void Close()
    {
        SetMenuOpen(false);
        _timeService.Resume();
    }

    private void ShowCards(List<ICardConfig> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            _cardsButtons[i].GetComponent<CardViewer>().Render(cards[i]);
            _cardsButtons[i].SetCard(cards[i]);
        }
    }

    private void SetMenuOpen(bool isOpen)
    {
        if (_isMenuOpen == isOpen)
            return;

        _isMenuOpen = isOpen;
        gameObject.SetActive(isOpen);
    }
}