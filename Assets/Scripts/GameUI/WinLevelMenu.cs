using UnityEngine;
using UnityEngine.UI;
using YG;

public class WinLevelMenu : FinishMenu
{
    public override void OnNextLevelClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        GameStateMachine.EnterIn<LoadingLevelState, LevelID>(CurrentLevel + 1);
        CloseMenu();
    }
}