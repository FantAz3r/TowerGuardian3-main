using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : PauseWindow
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _continueButton;

    private IStateSwitchService _stateMachine;
    private IWindowService _windowService;

    protected override void Awake()
    {
        base.Awake();
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

        base.Close();
        _windowService.Open(WindowType.HUD);

        if (SceneManager.GetActiveScene().name != LevelID.Tower.ToString())
        {
            _stateMachine.Switch(LevelID.Tower);
        }
    }
}
