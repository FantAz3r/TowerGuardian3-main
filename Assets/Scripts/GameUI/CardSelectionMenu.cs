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
    [SerializeField] private UIDummy _panel;

    private GameUI _uiRoot;
    private List<CardButton> _cardsButtons;
    private PlayerExperience _playerExperience;
    private CardSelector _selector;
    private ITimeService _timeService;
    private int _selectCount;

    public void Init(PlayerExperience playerExperience, CardSelector selector, List<CardButton> cardsButtons, GameUI uiRoot)
    {
        _uiRoot = uiRoot;
        _playerExperience = playerExperience;
        _selector = selector;
        _cardsButtons = cardsButtons;
        _timeService = ServicesLocator.GetService<ITimeService>();

        LoadUpgradeScore();
        _showButton.gameObject.SetActive(_selectCount > 0);
        _text.text = _selectCount.ToString();

        _playerExperience.OnLevelUp += AddPoints;

        GridLayoutGroup panel = GetComponentInChildren<GridLayoutGroup>();

        foreach (var button in _cardsButtons)
        {
            button.Selected += Close;
            button.transform.SetParent(panel.transform);
        }

        _panel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _playerExperience.OnLevelUp -= AddPoints;

        foreach (var button in _cardsButtons)
        {
            button.Selected -= Close;
        }
    }

    public void Open()
    {
        List<ICardConfig> cards = _selector.GetCards().ToList();

        if (cards.Count > 0)
        {
            if (_selectCount < 0)
                return;

            MenuOpen();
            _uiRoot.HUD.Disable();
            ShowCards(cards);
        }
    }

    public void Close()
    {
        _selectCount--;
        _text.text = _selectCount.ToString();

        if (_selectCount > 0)
        {
            Open();
        }
        else
        {
            ShowButton();
            CloseMenu();
            _uiRoot.HUD.Enable();
            SaveUpgradeScore();
        }
    }

    public void AddPoints(int points)
    {
        _selectCount += points;
        _showButton.gameObject.SetActive(true);
        _text.text = _selectCount.ToString();
        SaveUpgradeScore();
    }

    private void ShowCards(List<ICardConfig> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            _cardsButtons[i].gameObject.SetActive(true);
            _cardsButtons[i].GetComponent<CardViewer>().Render(cards[i]);
            _cardsButtons[i].SetCard(cards[i]);
        }
    }

    private void MenuOpen()
    {
        _panel.gameObject.SetActive(true);
        _timeService.PauseGame();
    }

    public void CloseMenu()
    {
        _panel.gameObject.SetActive(false);
        _timeService.Resume();
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