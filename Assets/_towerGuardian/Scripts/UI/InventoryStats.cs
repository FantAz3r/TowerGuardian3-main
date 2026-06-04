using TMPro;
using TowerGuardian.Enums;
using TowerGuardian.StaticData;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStats : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _stats;
    [SerializeField] private TMP_Text _level;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void View(ICardConfig config)
    {
        gameObject.SetActive(true);
        _image.sprite = config.Icon;
        _nameText.text = config.Name;
        _descriptionText.text = config.Description;
        _level.text = config.Level.ToString();
        InitStats(config);
    }

    private void InitStats(ICardConfig config)
    {
        _stats.text = string.Empty;

        foreach (var item in config.GetStats())
        {
            _stats.text += $"{item.Name}: {item.NextValue:0.#} \n";
        }

        if (config is WeaponConfig weaponConfig && weaponConfig.TargetType != EntityType.Generic)
        {
            string targetTypeName = UIText.GetEntityTypeText(weaponConfig.TargetType);
            _stats.text += $"{UIText.Multiplier} {targetTypeName}: {weaponConfig.GetMultiply(config.Level):0.#} \n";
        }
    }
}