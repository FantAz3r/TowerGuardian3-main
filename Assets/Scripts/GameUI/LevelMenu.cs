using UnityEngine;
using UnityEngine.UI;

public abstract class LevelMenu : PauseWindow
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    public LevelID _currentLevel { get; private set; }
    public ScoreCounter ScoreCounter { get; private set; }
    public IStateSwitchService StateSwitchService { get; private set; }
    public IGameFactory GameFactory { get; private set; }
    public IWindowService WindowService { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        WindowService = ServiceLocator.Get<IWindowService>();   
        StateSwitchService = ServiceLocator.Get<IStateSwitchService>();
        GameFactory = ServiceLocator.Get<IGameFactory>();

        ScoreCounter = GameFactory.ScoreCounter;
        _currentLevel = GameFactory.LevelConfig.Level;
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