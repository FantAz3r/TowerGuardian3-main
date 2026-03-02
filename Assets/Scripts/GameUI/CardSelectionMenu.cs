using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CardSelectionMenu : PauseWindow
{
    [SerializeField] private RectTransform _buttonsParent;
    [SerializeField] private TMP_Text _levelText;

    private List<CardButton> _cardsButtons;
    private CardSelector _selector;
    private Player _player;
    private List<ICardConfig> _currentCards;
    private IWindowService _windowService;
    private ITimeService _timeService;

    protected override void Awake()
    {
        base.Awake();
        _selector = new CardSelector(Resources.Load<CardData>(GameConstants.CardData));
        _windowService = ServiceLocator.Get<IWindowService>();
        _timeService = ServiceLocator.Get<ITimeService>();
        _player = ServiceLocator.Get<IGameFactory>().Player;
    }

    private void OnDisable()
    {
        _cardsButtons.Clear();

        foreach (var button in _cardsButtons)
        {
            button.Selected -= CloseMenu;
            Destroy(button.gameObject);
        }
    }

    public override void Open()
    {
        base.Open();
        OpenMenu();
    }

    public void OpenMenu()
    {
        _levelText.text = _player.Experience.CurrentLevel.ToString();

        if (_currentCards == null)
        {
            _currentCards = _selector.GetCards().ToList();
        }

        if (_currentCards.Count == 0)
            return;

        _cardsButtons = CreateCards();
        ShowCards(_currentCards);
    }

    public void CloseMenu()
    {
        _player.Experience.RemoveUpgradePoint(1);
        DestroyCards();

        if (_player.Experience.UpgradePoints > 0)
        {
            OpenMenu();
        }
        else
        {
            base.Close();
            _timeService.SlowMotion(0, 0);
            _windowService.Open(WindowType.HUD);
            _timeService.SlowMotion(1, 1);
        }
    }

    public void PostponeChoise()
    {
        base.Close();
        _timeService.SlowMotion(0, 0);
        _windowService.Open(WindowType.HUD);
        _timeService.SlowMotion(1, 1);
    }

    private void ShowCards(List<ICardConfig> cards)
    {
        for (int i = 0; i < _cardsButtons.Count; i++)
        {
            if (i < cards.Count)
            {
                _cardsButtons[i].gameObject.SetActive(true);
                _cardsButtons[i].GetComponent<CardViewer>().Render(cards[i]);
                _cardsButtons[i].SetCard(cards[i]);
            }
            else
            {
                _cardsButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private List<CardButton> CreateCards()
    {
        int maxCardCount = 3;
        List<CardButton> cards = new List<CardButton>();
        CardButton prefab = Resources.Load<CardButton>(GameConstants.Card);

        for (int i = 0; i < maxCardCount; i++)
        {
            CardButton card = Instantiate(prefab, _buttonsParent);
            card.Init(_player.CardHolder);
            cards.Add(card);
            card.Selected += CloseMenu;
        }

        return cards;
    }

    private void DestroyCards()
    {
        foreach (var cardButton in _cardsButtons)
        {
            Destroy(cardButton.gameObject);
        }

        _currentCards = null;
    }
}