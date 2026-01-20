using System;
using YG;

public class LoadingLevelState : IPayloadedState<LevelID>
{
    private readonly ICoroutineRunner _coroutineRunner;

    private SceneLoader _sceneLoader;
    private UIFactory _uiFactory;
    private GameFactory _gameFactory;
    private LevelID _currentLevel;
    private LevelID _previousLevel;

    public LoadingLevelState( ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void Enter(LevelID level)
    {
        CteateFactories();
        InitCurrentLevel(level);

        if(level != LevelID.MainMenu)
        {
            YG2.saves.CurrentLevel = level;
            YG2.SaveProgress();
        }
    }

    public void Exit()
    {
        _gameFactory.ClearSpawners();
    }

    private void InitCurrentLevel(LevelID level)
    {
        _previousLevel = _currentLevel;
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
                _sceneLoader.Load(level.ToString(), InitGameLevel);
                break;

            case LevelID.Level4:
                _sceneLoader.Load(level.ToString(), InitGameLevel);
                break;

            case LevelID.Tower:
                _sceneLoader.Load(level.ToString(), InitTowerLevel);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(level));
        }
    }

    private void CteateFactories()
    {
        _sceneLoader = new SceneLoader(_coroutineRunner);
        _uiFactory = new UIFactory();
        _gameFactory = new GameFactory();
    }

    private void InitMainMenu()
    {
        _uiFactory.CreateFocusController();
        _uiFactory.CreateUIRoot();
        _uiFactory.CreateSounds();
        _uiFactory.CreateSettings();
        _uiFactory.CreateBackgroundSounds();
    }

    private void InitGameLevel()
    {
        _gameFactory.CreateFocusController();
        _gameFactory.SetLevelConfig(_currentLevel);
        _gameFactory.CreatePlayer(_previousLevel);
        _gameFactory.CreateSpawners();
        _gameFactory.CreateWeaponFactory();
        _gameFactory.CreateCamera();
        _gameFactory.CreateEventSystem();
        _gameFactory.CreateCards();
        _gameFactory.CreateLight();
        _gameFactory.CreateEnemies();
        _gameFactory.CreateScoreCounter();

        _gameFactory.CreateUI();
        _gameFactory.CreateHUD();
        _gameFactory.InitUIWindows();

        _gameFactory.CreateCardButtons();
        _gameFactory.CreateCardsSelectionMenu();

        _gameFactory.CreatePortalsFactory();
        _gameFactory.CreateQuests();
        _gameFactory.CreateTutorial();

        _gameFactory.CreateBackgroundSounds();
    }

    private void InitTowerLevel()
    {
        _gameFactory.CreateFocusController();
        _gameFactory.SetLevelConfig(_currentLevel);
        _gameFactory.CreateLight();
        _gameFactory.CreatePlayer(_previousLevel);
        _gameFactory.CreateEventSystem();
        _gameFactory.CreateSpawners();
        _gameFactory.CreateTower();

        _gameFactory.CreateScoreCounter();
        _gameFactory.CreateWeaponFactory();
        _gameFactory.CreateCamera();
        _gameFactory.CreateCards();

        _gameFactory.CreateUI();
        _gameFactory.CreateHUD();
        _gameFactory.InitUIWindows();

        _gameFactory.CreateCardButtons();
        _gameFactory.CreateCardsSelectionMenu();

        _gameFactory.CreateActions();
        _gameFactory.CreatePortalsFactory();
        _gameFactory.CreateQuests();
        _gameFactory.CreateTutorial();

        _gameFactory.InitPlatforms();
        _gameFactory.CreateBackgroundSounds();
    }
}