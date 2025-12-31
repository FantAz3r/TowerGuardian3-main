using UnityEngine;
using UnityEngine.UI;
using YG;

public class ProgressReseter : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ResetProgress);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ResetProgress);
    }

    private void ResetProgress()
    {

        if (YG2.saves.AllCards != null) YG2.saves.AllCards.Clear();
        if (YG2.saves.PlayerWeapons != null) YG2.saves.PlayerWeapons.Clear();

        YG2.saves.Coins = 0;
        YG2.saves.Wood = 0;
        YG2.saves.Stones = 0;

        YG2.saves.Level = 0;
        YG2.saves.UpgradePoints = 0;
        YG2.saves.CurrentEXP = 0f;
        YG2.saves.CurrentLevel = null;

        if (YG2.saves.PlayerPositions != null) YG2.saves.PlayerPositions.Clear();
        if (YG2.saves.LevelsProgress != null) YG2.saves.LevelsProgress.Clear();

        YG2.SaveProgress();
    }
}
