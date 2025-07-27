using System;
using UnityEngine;

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

    public LoadingLevelState( AllServices services, ICoroutineRunner coroutineRunner)
    {
        _services = services;
        _coroutineRunner = coroutineRunner;
    }

    public void Enter(LevelID level)
    {
        InitServices();
        CteateFactories();
        InitCurrentLevel(level);
    }

    public void Exit()
    {
        
    }

    private void InitCurrentLevel(LevelID level)
    {
        switch (level)
        {
            case LevelID.MainMenu:
                _sceneLoader.Load(level.ToString(), InitMainMenu); 
                break;

            case LevelID.Level1:
                _sceneLoader.Load(level.ToString(), InitGameLevel); 
                break;

            case LevelID.Level2:
                break;

            case LevelID.Level3:
                break;

            case LevelID.Level4:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(level));
        }
    }

    private void CteateFactories()
    {
        _sceneLoader = new SceneLoader(_coroutineRunner);
        _uiFactory = new UIFactory(_stateSwithService);
        _gameFactory = new GameFactory(_inputService, _timeService);

    }

    private void InitServices()
    {
        _levelLoadingService = _services.GetService<ILevelLoadingService>();
        _timeService = _services.GetService<ITimeService>();
        _stateSwithService = _services.GetService<IStateSwitchService>();
        _inputService = _services.GetService<IInputService>();
    }

    private void InitMainMenu()
    {
        _uiFactory.CreateUIRoot();
        _uiFactory.CreateStartButton();
    }

    private void InitGameLevel()
    {
        _gameFactory.CreatePlayer();
        _gameFactory.CreateWeaponFactory();
        _gameFactory.CreateCamera();
        _gameFactory.CreateUI();
        _gameFactory.CreateResourceView();
        _gameFactory.CreateCards();
        _gameFactory.CreateCardButtons();
        _gameFactory.CreateCardsSelectionMenu();
    }
}