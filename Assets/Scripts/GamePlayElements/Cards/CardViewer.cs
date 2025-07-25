using TMPro;
using UnityEngine;

public class CardViewer : MonoBehaviour
{
    [SerializeField, Range(1, 3)] private int _cardIndex = 1;
    [SerializeField] private Sprite _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _stats;
    private CardSelectionMenu _selectionMenu;

    private void Awake()
    {
        _selectionMenu = GetComponentInParent<CardSelectionMenu>();
    }

    private void OnEnable()
    {
        _selectionMenu.CardLoaded += OnCardLoaded;
    }

    private void OnDisable()
    {
        _selectionMenu.CardLoaded -= OnCardLoaded;
    }

    private void OnCardLoaded(int index, IConfig config)
    {
        if (index == _cardIndex)
            Render(config);
    }

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