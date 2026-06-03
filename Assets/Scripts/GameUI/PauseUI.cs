using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : PauseWindow
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _continueButton;

    private IStateSwitchService _stateMachine;
    private IWindowService _windowService;
    private IADVServise _advServise;

    protected override void Awake()
    {
        base.Awake();
        _advServise = ServiceLocator.Get<IADVServise>();
        _windowService = ServiceLocator.Get<IWindowService>();
        _stateMachine = ServiceLocator.Get<IStateSwitchService>();
    }

    private void OnEnable()
    {
        _homeButton.onClick.AddListener(OnHomeClicked);
        _continueButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _homeButton.onClick.RemoveListener(OnHomeClicked);
        _continueButton.onClick.RemoveListener(Close);
    }

    private void OnHomeClicked()
    {
        Close();
        _windowService.Open(WindowType.HUD);
        string levelName = SceneManager.GetActiveScene().name;

        if (levelName != LevelID.Tower.ToString())
        {
            _stateMachine.Switch(LevelID.Tower);
            _advServise.TryShowInterstitialADV(levelName);
        }
    }
}
