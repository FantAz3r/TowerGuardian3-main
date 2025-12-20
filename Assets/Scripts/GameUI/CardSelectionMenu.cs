using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class CardSelectionMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _showButton;
    [SerializeField] private RectTransform _panel;

    private List<CardButton> _cardsButtons;
    private PlayerExperience _playerExperience;
    private bool _isMenuOpen = false;
    private CardSelector _selector;
    private int _selectCount;

    private void Start()
    {
        _showButton.gameObject.SetActive(false);
        _panel.gameObject.SetActive(false);
    }

    public void Init(PlayerExperience playerExperience, CardSelector selector, List<CardButton> cardsButtons)
    {
        _playerExperience = playerExperience;
        _selector = selector;
        _cardsButtons = cardsButtons;

        LoadUpgradeScore();
        _playerExperience.OnLevelUp += AddSelect;

        foreach (var button in _cardsButtons)
        {
            button.Selected += Close;
            GridLayoutGroup panel = GetComponentInChildren<GridLayoutGroup>();
            button.transform.SetParent(panel.transform);
        }
    }

    private void OnDestroy()
    {
        _playerExperience.OnLevelUp -= AddSelect;

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
            if (_selectCount < 0)
                return;

            SetMenuOpen(true);
            YG2.PauseGameNoEditEventSystem(true);
            ShowCards(cards);
        }
    }

    public void Close()
    {
        _selectCount--;
        _text.text = _selectCount.ToString();
        ShowButton();

        SetMenuOpen(false);
        YG2.PauseGameNoEditEventSystem(false);
        SaveUpgradeScore();
    }

    private void AddSelect(int level)
    {
        _selectCount++;
        _showButton.gameObject.SetActive(true);
        _text.text = _selectCount.ToString();
        SaveUpgradeScore();
    }

    private void ShowCards(List<ICardConfig> cards)
    {
        int maxShowedCards = 3;

        for (int i = 0; i < maxShowedCards; i++)
        {
            _cardsButtons[i].gameObject.SetActive(true);

            if (i < cards.Count)
            {
                _cardsButtons[i].GetComponent<CardViewer>().Render(cards[i]);
                _cardsButtons[i].SetCard(cards[i]);
            }
            else
            {
                _cardsButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetMenuOpen(bool isOpen)
    {
        if (_isMenuOpen == isOpen)
            return;

        _isMenuOpen = isOpen;
        _panel.gameObject.SetActive(isOpen);
    }

    private void ShowButton()
    {
        _showButton.gameObject.SetActive(_selectCount > 0);
    }

    private void SaveUpgradeScore()
    {
        YG2.saves.UpgradePoints = _selectCount;
    }

    private void LoadUpgradeScore()
    {
        _selectCount = YG2.saves.UpgradePoints;
    }
}