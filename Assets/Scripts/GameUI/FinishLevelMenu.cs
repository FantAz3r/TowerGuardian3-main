using UnityEngine;
using UnityEngine.UI;
using YG;

public class FinishLevelMenu : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _restartButton;

    private GameStateMachine _gameStateMachine;
    private LevelID _currentLevel;

    public void Init(GameStateMachine gameStateMachine, LevelID level)
    {
        _gameStateMachine = gameStateMachine;
    }

    private void Awake()
    {
        _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        _restartButton.onClick.AddListener(OnRestartClicked);
        gameObject.SetActive(false);
    }

    public void LevelEnd(Health health)
    {
        gameObject.SetActive(true);
        YG2.PauseGameNoEditEventSystem(true);
    }

    private void OnNextLevelClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(_currentLevel + 1);
        CloseMenu();
    }

    private void OnRestartClicked()
    {
        YG2.PauseGameNoEditEventSystem(false);
        _gameStateMachine.EnterIn<LoadingLevelState, LevelID>(_currentLevel);
        CloseMenu();
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
        _restartButton.onClick.RemoveListener(OnRestartClicked);
    }
}


