using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private RectTransform _pausePanel;

    private IStateSwitchService _stateMachine;
    private ITimeService _timeService;
    private LevelID _currentLevel;

    private void Awake()
    {
        _stateMachine = ServicesLocator.GetService<IStateSwitchService>();
        _timeService = ServicesLocator.GetService<ITimeService>();
        _homeButton.onClick.AddListener(OnHomeClicked);
        _continueButton.onClick.AddListener(OnContinue);
        _pauseButton.onClick.AddListener(OnPause);
        _pausePanel.gameObject.SetActive(false);
    }

    public void Init(LevelID currentLevel)
    {
        _currentLevel = currentLevel;
    }

    private void OnDestroy()
    {
        _homeButton.onClick.RemoveListener(OnHomeClicked);
        _continueButton.onClick.RemoveListener(OnContinue);
    }

    private void OnHomeClicked()
    {
        OnContinue();

        if (_currentLevel != LevelID.Tower)
        {
            _stateMachine.Switch(LevelID.Tower);
        }
    }

    public void OnPause()
    {
        _timeService.PauseGame();
    }

    private void OnContinue()
    {
        _timeService.Resume();
        _pausePanel.gameObject.SetActive(false);
    }
}
