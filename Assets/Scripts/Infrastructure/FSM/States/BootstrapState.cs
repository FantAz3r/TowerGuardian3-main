public class BootstrapState : IState
{
    private IGameStateMachine _stateMachine;
    private readonly AllServices _services;
    private readonly SceneLoader _sceneLoader;
    private ICoroutineRunner _coroutineRunner;

    public BootstrapState(IGameStateMachine stateMachine, AllServices services, SceneLoader sceneLoader, ICoroutineRunner coroutineRunner)
    {
        _stateMachine = stateMachine;
        _services = services;
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
        _services.Register<IStateSwitchService>(new StateSwitchService(_stateMachine));
        _services.Register<ILevelLoadingService>(new LevelLoadingService(_stateMachine));
        _services.Register<IInputService>(new InputService());
        _services.Register<ITimeService>(new TimeService(_coroutineRunner));
    }
}
