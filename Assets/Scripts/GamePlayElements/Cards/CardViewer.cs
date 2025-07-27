using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardViewer : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _stats;

    public void Render(IConfig config)
    {
        _icon = config.Icon;
        _nameText.text = config.Name;
        _descriptionText.text = config.Description;
        InitStats(config);
    }

    private void InitStats(IConfig config)
    {
        _stats.text = "";

        foreach (var item in config.GetStats())
        {
            _stats.text += $"{item.Key}: {item.Value}\n";
        }

        if (config is WeaponConfig weaponConfig && weaponConfig.TargetType != TargetType.Generic)
        {
            string targetTypeName = weaponConfig.TargetType.ToString();

            _stats.text += $"Damage Multiplier to {targetTypeName}: {weaponConfig.Multiply}\n";
        }
    }
}