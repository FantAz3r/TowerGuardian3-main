using UnityEngine;
using UnityEngine.UI;

public class WinLevelMenu : LevelMenu
{
    [SerializeField] private Button _nextLevelButton;

    protected override void Awake()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        base.Awake();
    }

    private void OnNextLevelClicked()
    {
        StateSwitchService.Switch(LevelID.Tower);
        CloseMenu();
    }

    protected override void OpenMenu()
    {
        base.OpenMenu();
        ScoreCounter.OnEndLevel();
    }

    protected override void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
        base.OnDestroy();
    }
}