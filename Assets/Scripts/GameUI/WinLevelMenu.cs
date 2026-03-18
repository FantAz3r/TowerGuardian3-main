using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinLevelMenu : LevelMenu
{

    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private TMP_Text _levelNumberText;

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
        base.Close();
    }

    public override void Open()
    {
        base.Open();
        _levelNumberText.text = ((int)CurrentLevel - EnumGameLevelOffset).ToString();
        ScoreCounter.OnEndLevel(this);
        ServiceLocator.Get<ISpawnerService>().SendSoundReqest(_winSound);
    }
}