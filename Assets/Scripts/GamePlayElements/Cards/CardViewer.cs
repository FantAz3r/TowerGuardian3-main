using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardViewer : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _stats;
    [SerializeField] private TMP_Text _level;

    public void Render(ICardConfig config)
    {
        _icon.sprite = config.Icon;
        _nameText.text = config.Name;
        _descriptionText.text = config.Description;
        _level.text = $"LVL {config.Level}";
        InitStats(config);
    }

    private void InitStats(ICardConfig config)
    {
        _stats.text = "";

        if (config.Level == 0)
        {
            foreach (var item in config.GetStats())
            {
                _stats.text += $"{item.Name}: {item.NextValue:0.#} \n";
            }
        }
        else
        {
            foreach (var item in config.GetStats())
            {
                string upgradeText = ShowUpgrade(config, item.Value, item.NextValue);
                _stats.text += $"{item.Name}: {item.Value:0.#} {upgradeText}\n";
            }
        }

        if (config is WeaponConfig weaponConfig && weaponConfig.TargetType != EntityType.Generic)
        {
            string targetTypeName = weaponConfig.TargetType.ToString();
            _stats.text += $"Multiplier to {targetTypeName}: {weaponConfig.GetMultiply(config.Level):0.#} \n";
        }
    }

    private string ShowUpgrade(ICardConfig config, float value, float nextValue)
    {
        float difference = nextValue - value;

        if (difference > 0)
            return $"<color=green>+{difference:0.#}</color>";
        else if (difference < 0)
            return $"<color=red>{difference:0.#}</color>";

        return "";
    }
}