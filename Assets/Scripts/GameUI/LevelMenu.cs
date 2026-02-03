using UnityEngine;
using UnityEngine.UI;

public abstract class LevelMenu : PauseWindow
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    private ScoreCounter _scoreCounter;
    private IStateSwitchService _stateSwitchService;
    private LevelID _currentLevel;

    public ScoreCounter ScoreCounter => _scoreCounter;
    public IStateSwitchService StateSwitchService => _stateSwitchService;

    public virtual void Init(ScoreCounter scoreCounter, LevelID currentLevel)
    {
        _scoreCounter = scoreCounter;
        _currentLevel = currentLevel;
    }

    protected override void Awake()
    {
        base.Awake();
        _stateSwitchService = ServiceLocator.Get<IStateSwitchService>();
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
        _stateSwitchService.Switch(_currentLevel);
        Close();
    }

    protected virtual void OnHomeClicked()
    {
        _stateSwitchService.Switch(LevelID.Tower);
        Close();
    }
}