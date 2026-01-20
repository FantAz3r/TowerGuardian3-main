public class BootstrapState : IState
{
    private IGameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private ICoroutineRunner _coroutineRunner;

    public BootstrapState(IGameStateMachine stateMachine, SceneLoader sceneLoader, ICoroutineRunner coroutineRunner)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter()
    {
        RegisterServices();
        _stateMachine.EnterIn<LoadingLevelState, LevelID>(LevelID.MainMenu);
    }

    public void Exit()
    {

    }

    private void RegisterServices()
    {
        ServicesLocator.Register<IStateSwitchService>(new StateSwitchService(_stateMachine));
        ServicesLocator.Register<ILevelLoadingService>(new LevelLoadingService(_stateMachine));
        ServicesLocator.Register<IInputService>(new InputService());
        ServicesLocator.Register<ITimeService>(new TimeService(_coroutineRunner));
        ServicesLocator.Register<ISpawnerService>(new SpawnerService());
        ServicesLocator.Register<ICoroutineRunner>(_coroutineRunner);
    }
}
