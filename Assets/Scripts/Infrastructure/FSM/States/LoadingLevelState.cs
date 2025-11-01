using System;
using YG;

public class LoadingLevelState : IPayloadedState<LevelID>
{
    private readonly ICoroutineRunner _coroutineRunner;
    private readonly AllServices _services;
    private SceneLoader _sceneLoader;
    private UIFactory _uiFactory;
    private GameFactory _gameFactory;
    private ILevelLoadingService _levelLoadingService;
    private IStateSwitchService _stateSwithService;
    private IInputService _inputService;
    private ITimeService _timeService;
    private ISpawnerService _spawnerService;
    private LevelID _currentLevel;
    private GameStateMachine _gameStateMachine;

    public LoadingLevelState(AllServices services, ICoroutineRunner coroutineRunner, GameStateMachine stateMachine)
    {
        _services = services;
        _coroutineRunner = coroutineRunner;
        _gameStateMachine = stateMachine;
    }

    public void Enter(LevelID level)
    {
        InitServices();
        CteateFactories();
        InitCurrentLevel(level);
    }

    public void Exit()
    {
        _gameFactory.ClearSpawners();
    }

    private void InitCurrentLevel(LevelID level)
    {
        _currentLevel = level;

        switch (level)
        {
            case LevelID.MainMenu:
                _sceneLoader.Load(level.ToString(), InitMainMenu);
                break;

            case LevelID.Level1:
                _sceneLoader.Load(level.ToString(), InitGameLevel);
                break;

            case LevelID.Level2:
                _sceneLoader.Load(level.ToString(), InitGameLevel);
                break;

            case LevelID.Level3:
                break;

            case LevelID.Level4:
                break;

            case LevelID.Tower:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(level));
        }
    }

    private void CteateFactories()
    {
        _sceneLoader = new SceneLoader(_coroutineRunner);
        _uiFactory = new UIFactory(_stateSwithService);
        _gameFactory = new GameFactory(_inputService, _timeService, _spawnerService, _gameStateMachine);
    }

    private void InitServices()
    {
        _levelLoadingService = _services.GetService<ILevelLoadingService>();
        _timeService = _services.GetService<ITimeService>();
        _stateSwithService = _services.GetService<IStateSwitchService>();
        _inputService = _services.GetService<IInputService>();
        _spawnerService = _services.GetService<ISpawnerService>();
    }

    private void InitMainMenu()
    {
        _uiFactory.CreateUIRoot();
        _uiFactory.CreateStartButton();
    }

    private void InitGameLevel()
    {
        _gameFactory.CreatePlayer();
        _gameFactory.CreateSpawners(_spawnerService);
        _gameFactory.CreateWeaponFactory();
        _gameFactory.CreateCamera();
        _gameFactory.CreateEventSystem();
        _gameFactory.CreateUI();
        _gameFactory.CreateResourceView();
        _gameFactory.CreateCards();
        _gameFactory.CreateShop();
        _gameFactory.CreateCardButtons();
        _gameFactory.CreateCardsSelectionMenu();
        _gameFactory.CreateLight(_currentLevel);
        _gameFactory.CreateEnemies(_currentLevel);
        _gameFactory.CreateAbilityPanel();
        _gameFactory.CreateWeaponPanel();
        _gameFactory.CreateEndLevelMenu(_currentLevel);
        _gameFactory.CreatePortalsFactory();
        _gameFactory.CreateActions();
        _gameFactory.CreatePlatform();
        _gameFactory.CreateQuests();
        _gameFactory.CreateTutorial();
    }
}