using UnityEngine;
using UnityEngine.UI;
using YG;

public abstract class FinishMenu : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;

    private GameStateMachine _gameStateMachine;
    private LevelID _currentLevel;

    public GameStateMachine GameStateMachine => _gameStateMachine;
    public LevelID CurrentLevel => _currentLevel;

    public void Init(GameStateMachine gameStateMachine, LevelID level)
    {
        _gameStateMachine = gameStateMachine;
        _currentLevel = level;
    }

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        _restartButton.onClick.AddListener(OnRestartClicked);
        gameObject.SetActive(false);
    }

    public void LevelEnd()
    {
        YG2.PauseGameNoEditEventSystem(true);
        gameObject.SetActive(true);
    }

    public virtual void OnNextLevelClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        CloseMenu();
    }

    private void OnRestartClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(_currentLevel);
        CloseMenu();
    }

    protected void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
        _restartButton.onClick.RemoveListener(OnRestartClicked);
    }
}