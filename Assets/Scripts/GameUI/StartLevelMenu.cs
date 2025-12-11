using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelMenu : LevelMenu
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _levelText;

    private LevelID _portalLevel;
    public LevelID PortalLevel => _portalLevel;

    protected override void Awake()
    {
        base.Awake();
        _startButton.onClick.AddListener(OnStartClicked);
    }

    public void SetPortalLevel(LevelID level)
    {
        _portalLevel = level;
        _levelText.text = level.ToString();
        OpenMenu();
    }

    protected override void OpenMenu()
    {
        base.OpenMenu();
        ScoreCounter.OnEndLevel(_portalLevel);
    }

    private void OnStartClicked()
    {
        GameStateMachine.EnterIn<LoadingLevelState, LevelID>(_portalLevel);
        CloseMenu();
    }

    protected override void OnHomeClicked()
    {
        CloseMenu();
    }

    protected override void OnDestroy()
    {
        _startButton.onClick.RemoveListener(OnStartClicked);
        base.OnDestroy();
    }
}

