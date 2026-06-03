using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelMenu : LevelMenu
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private TMP_Text _levelScoreInfo;

    private LevelID _nextLevel;

    public void Init(LevelID nextLevel)
    {
        _nextLevel = nextLevel;
        _levelNumberText.text = ((int)nextLevel - EnumGameLevelOffset).ToString();
        ShowScoreInfo();
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
        ScoreCounter.OnEndLevel(this, _nextLevel);
    }

    private void OnStartClicked()
    {
        StateSwitchService.Switch(_nextLevel);
        ADVServise.TryShowInterstitialADV(_nextLevel.ToString());
        ConditionService.SetLevelEnded();
        Close();
    }

    protected override void OnHomeClicked()
    {
        Close();
    }

    private void ShowScoreInfo()
    {
        if (ScoreCounter.HasScoreInfo(_nextLevel))
        {
            _levelScoreInfo.text = UIText.YourBestScore;
        }
        else
        {
            _levelScoreInfo.text = UIText.NoBestScore;
        }
    }
}