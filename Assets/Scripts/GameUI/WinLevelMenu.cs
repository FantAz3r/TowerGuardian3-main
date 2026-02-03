using UnityEngine;
using UnityEngine.UI;

public class WinLevelMenu : LevelMenu
{
    [SerializeField] private Button _nextLevelButton;

    public override void Init(ScoreCounter scoreCounter, LevelID currentLevel)
    {
        base.Init(scoreCounter, currentLevel);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _nextLevelButton.onClick.AddListener(OnNextLevelClicked);
    }

    protected override void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
        base.OnDisable();
    }

    private void OnNextLevelClicked()
    {
        StateSwitchService.Switch(LevelID.Tower);
        Close();
    }

    public override void Open()
    {
        base.Open();
        ScoreCounter.OnEndLevel();
    }
}