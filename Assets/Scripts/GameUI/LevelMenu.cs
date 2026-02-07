using UnityEngine;
using UnityEngine.UI;

public abstract class LevelMenu : PauseWindow
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    private LevelID _currentLevel;

    public ScoreCounter ScoreCounter { get; private set; }
    public IStateSwitchService StateSwitchService { get; private set; }

    public virtual void Init(ScoreCounter scoreCounter, LevelConfig levelConfig)
    {
        ScoreCounter = scoreCounter;
        _currentLevel = levelConfig.Level;
    }

    protected override void Awake()
    {
        base.Awake();
        StateSwitchService = ServiceLocator.Get<IStateSwitchService>();
    }

    protected virtual void OnEnable()
    {
        if(_restartButton != null)
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        if (_homeButton != null)
        {
            _homeButton.onClick.AddListener(OnHomeClicked);
        }
    }

    protected virtual void OnDisable()
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

    private void OnRestartClicked()
    {
        StateSwitchService.Switch(_currentLevel);
        Close();
    }

    protected virtual void OnHomeClicked()
    {
        StateSwitchService.Switch(LevelID.Tower);
        Close();
    }
}