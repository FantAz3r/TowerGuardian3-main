using UnityEngine;
using UnityEngine.UI;
using YG;

public abstract class FinishMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    private GameStateMachine _gameStateMachine;
    private LevelID _currentLevel;

    public GameStateMachine GameStateMachine => _gameStateMachine;
    public LevelID CurrentLevel => _currentLevel;

    public void Init(GameStateMachine gameStateMachine, LevelID level)
    {
        _gameStateMachine = gameStateMachine;
        _currentLevel = level;
    }

    protected virtual void Awake()
    {
        _restartButton.onClick.AddListener(OnRestartClicked);
        _homeButton.onClick.AddListener(OnHomeClicked);
        gameObject.SetActive(false);
    }

    public void LevelEnd()
    {
        YG2.PauseGameNoEditEventSystem(true);
        gameObject.SetActive(true);
    }

    private void OnRestartClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(_currentLevel);
        CloseMenu();
    }

    private void OnHomeClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(LevelID.Tower);
        CloseMenu();
    }

    protected void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnDestroy()
    {
        _restartButton.onClick.RemoveListener(OnRestartClicked);
        _homeButton.onClick.RemoveListener(OnHomeClicked);
    }
}