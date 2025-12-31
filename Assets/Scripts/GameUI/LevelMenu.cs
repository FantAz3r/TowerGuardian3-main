using UnityEngine;
using UnityEngine.UI;
using YG;

public abstract class LevelMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    private GameUI _gameUI;
    private ScoreCounter _scoreCounter;
    private GameStateMachine _gameStateMachine;
    private LevelID _currentLevel;

    public ScoreCounter ScoreCounter => _scoreCounter;
    public GameUI GameUI => _gameUI;
    public GameStateMachine GameStateMachine => _gameStateMachine;
    public LevelID CurrentLevel => _currentLevel;

    public virtual void Init(GameStateMachine gameStateMachine, ScoreCounter scoreCounter, GameUI gameUI, LevelID currentLevel)
    {
        _gameUI = gameUI;
        _gameStateMachine = gameStateMachine;
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
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(_currentLevel);
        CloseMenu();
    }

    protected virtual void OnHomeClicked()
    {
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(LevelID.Tower);
        CloseMenu();
    }

    protected virtual void OpenMenu()
    {
        gameObject.SetActive(true);
        _gameUI.HUD.gameObject.SetActive(false);
        YG2.PauseGameNoEditEventSystem(true);
    }

    protected void CloseMenu()
    {
        YG2.PauseGameNoEditEventSystem(false);
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