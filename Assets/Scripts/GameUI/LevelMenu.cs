using UnityEngine;
using UnityEngine.UI;

public abstract class LevelMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    private GameUI _gameUI;
    private ScoreCounter _scoreCounter;
    private IStateSwitchService _stateSwitchService;
    private ITimeService _timeService;
    private LevelID _currentLevel;

    public ScoreCounter ScoreCounter => _scoreCounter;
    public GameUI GameUI => _gameUI;
    public IStateSwitchService StateSwitchService => _stateSwitchService;
    public LevelID CurrentLevel => _currentLevel;

    public virtual void Init(ScoreCounter scoreCounter, GameUI gameUI, LevelID currentLevel)
    {
        _gameUI = gameUI;
        _stateSwitchService = ServicesLocator.GetService<IStateSwitchService>();
        _timeService = ServicesLocator.GetService<ITimeService>();
        _scoreCounter = scoreCounter;
        _currentLevel = currentLevel;
    }

    protected virtual void Awake()
    {
        if(_restartButton != null)
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        if (_homeButton != null)
        {
            _homeButton.onClick.AddListener(OnHomeClicked);
        }

        gameObject.SetActive(false);
    }

    public void LevelEnd(LevelID levelID)
    {
        _currentLevel = levelID;
        OpenMenu();
    }

    private void OnRestartClicked()
    {
        _stateSwitchService.Switch(_currentLevel);
        CloseMenu();
    }

    protected virtual void OnHomeClicked()
    {
        _stateSwitchService.Switch(LevelID.Tower);
        CloseMenu();
    }

    protected virtual void OpenMenu()
    {
        gameObject.SetActive(true);
        _gameUI.HUD.gameObject.SetActive(false);
        _timeService.PauseGame();
    }

    protected void CloseMenu()
    {
        _timeService.Resume();
        _gameUI.HUD.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        if (_restartButton != null)
        {
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        if (_homeButton != null)
        {
            _homeButton.onClick.RemoveListener(OnHomeClicked);
        }
    }
}