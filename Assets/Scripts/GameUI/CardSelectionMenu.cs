using System;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionMenu : MonoBehaviour
{
    [SerializeField] private List<CardViewer> _cardPanels;
    [SerializeField] private List<CardButton> _cardsButtons;

    private PlayerExperience _playerExperience;
    private ITimeService _timeService;
    private bool _isMenuOpen = false;
    private CardSelector _selector;

    public event Action<int, IConfig> CardLoaded;

    public void Init(ITimeService timeService, PlayerExperience playerExperience)
    {
        _timeService = timeService;
        _playerExperience = playerExperience;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _playerExperience.OnLevelUp += CardSelection;

        foreach (var button in _cardsButtons)
        {
            button.Selected += Close;
        }
    }

    private void OnDisable()
    {
        _playerExperience.OnLevelUp -= CardSelection;

        foreach (var button in _cardsButtons)
        {
            button.Selected -= Close;
        }
    }

    public void CardSelection(int level)
    {
        SetMenuOpen(true);
        _timeService.Pause();
        ShowCards(_selector.GetCards());
    }

    public void Close()
    {
        SetMenuOpen(false);
        _timeService.Resume();
    }

    private void ShowCards(List<ICard> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            CardLoaded?.Invoke(i, cards[i].Config);
        }
    }

    private void SetMenuOpen(bool isOpen)
    {
        if (_isMenuOpen == isOpen)
            return;

        _isMenuOpen = isOpen;
        gameObject.SetActive(isOpen);
        Cursor.visible = isOpen;

        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}