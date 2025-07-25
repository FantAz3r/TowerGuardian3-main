public class StateSwitchService : IStateSwitchService
{
    private IGameStateMachine _gameStateMachine;

    public StateSwitchService(IGameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Switch(LevelID state)
    {
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(state);
    }
}
