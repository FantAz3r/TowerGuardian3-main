using UnityEngine;
using UnityEngine.UI;
using YG;

public class NewGameButton : MonoBehaviour
{
    private LevelID _levelToLoad = LevelID.Tower;
    private Button _button;
    private IStateSwitchService _switchService;

    private void Awake()
    {
        _switchService = ServicesLocator.GetService<IStateSwitchService>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }
       
    public void OnClicked()
    {
        _button.interactable = false;
        ResetProgress();
        _switchService.Switch(_levelToLoad);
    }

    private void ResetProgress()
    {
        if (YG2.saves.AllCards != null) YG2.saves.AllCards.Clear();
        if (YG2.saves.PlayerWeapons != null) YG2.saves.PlayerWeapons.Clear();
        if (YG2.saves.LevelsProgress != null) YG2.saves.LevelsProgress.Clear();
        if (YG2.saves.QuestProgress != null) YG2.saves.QuestProgress.Clear();

        YG2.saves.Coins = 0;
        YG2.saves.Wood = 0;
        YG2.saves.Stones = 0;

        YG2.saves.Level = 0;
        YG2.saves.UpgradePoints = 0;
        YG2.saves.CurrentEXP = 0f;
        YG2.saves.CurrentLevel = LevelID.None;
        YG2.saves.CurrentFloor = 0;
        YG2.saves.PlayerPosition = Vector3.zero;
        YG2.SaveProgress();
    }
}
