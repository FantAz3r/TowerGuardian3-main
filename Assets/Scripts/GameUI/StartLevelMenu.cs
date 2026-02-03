using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelMenu : LevelMenu
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _levelText;

    private LevelID _nextLevel;

    public void Init(ScoreCounter scoreCounter, LevelID currentLevel, LevelID nextLevel)
    {
        base.Init(scoreCounter, currentLevel);
        _nextLevel = nextLevel;
        _levelText.text = nextLevel.ToString();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _startButton.onClick.AddListener(OnStartClicked);

    }

    protected override void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartClicked);
        base.OnDisable();
    }

    public override void Open()
    {
        base.Open();
        ScoreCounter.OnEndLevel(_nextLevel);
    }

    private void OnStartClicked()
    {
        StateSwitchService.Switch(_nextLevel);
        Close();
    }

    protected override void OnHomeClicked()
    {
        Close();
    }
}

