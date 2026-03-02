using UnityEngine;
using YG;

public class BootstrapState : IState
{
    private IGameStateMachine _stateMachine;
    private ICoroutineRunner _coroutineRunner;

    public BootstrapState(IGameStateMachine stateMachine, ICoroutineRunner coroutineRunner)
    {
        _stateMachine = stateMachine;
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
        ServiceLocator.Register<IStateSwitchService>(new StateSwitchService(_stateMachine));
        ServiceLocator.Register<ILevelLoadingService>(new LevelLoadingService(_stateMachine));

        ServiceLocator.Register<ITimeService>(new TimeService(_coroutineRunner));
        ServiceLocator.Register<ISpawnerService>(new SpawnerService());
        ServiceLocator.Register(_coroutineRunner);
        ServiceLocator.Register<IGameFactory>(new GameFactory());

        IWindowService windowService = new WindowService(new UIFactory());
        ServiceLocator.Register(windowService);
        ServiceLocator.Register<IGameConditionService>(new GameConditionService(windowService));
        ServiceLocator.Register<IScoreService>(new ScoreService());


        if (YG2.envir.isDesktop)
        {
            InputService inputService = new InputService();
            ServiceLocator.Register<IInputService>(inputService);
            ServiceLocator.Register<IAbilityInput>(inputService);
        }
        else
        {
            ServiceLocator.Register<IInputService>(new MobileInput());
        }
    }
}
