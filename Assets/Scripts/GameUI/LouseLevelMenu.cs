using YG;

public class LouseLevelMenu : FinishMenu
{
    public override void OnNextLevelClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        GameStateMachine.EnterIn<LoadingLevelState, LevelID>(LevelID.Tower);
        CloseMenu();
    }
}
