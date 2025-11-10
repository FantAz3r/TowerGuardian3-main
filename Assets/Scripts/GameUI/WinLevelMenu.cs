using UnityEngine;
using UnityEngine.UI;
using YG;

public class WinLevelMenu : FinishMenu
{
    [SerializeField] private Button _nextLevelButton;

    protected override void Awake()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        base.Awake();
    }

    private void OnNextLevelClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        GameStateMachine.EnterIn<LoadingLevelState, LevelID>(CurrentLevel + 1);
        CloseMenu();
    }

    protected override void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
        base.OnDestroy();
    }
}