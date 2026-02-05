using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardSelectionMenu : PauseWindow
{
    [SerializeField] private RectTransform _buttonsParent;

    private List<CardButton> _cardsButtons;
    private CardSelector _selector;
    private Player _player;
    private List<ICardConfig> _currentCards;
    private IWindowService _windowService; 

    protected override void Awake()
    {
        base.Awake();
        _selector = new CardSelector(Resources.Load<CardData>(GameConstants.CardData));
        _windowService = ServiceLocator.Get<IWindowService>();
    }

    public void Init(Player player)
    {
        _player = player;
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
        if (_currentCards == null)
        {
            _currentCards = _selector.GetCards().ToList();
        }

        if (_currentCards.Count == 0 || _player.Experience.UpgradePoints <= 0)
            return;

        _cardsButtons = CreateCards();

        base.Open();
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
            _windowService.Open(WindowType.HUD);
        }
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