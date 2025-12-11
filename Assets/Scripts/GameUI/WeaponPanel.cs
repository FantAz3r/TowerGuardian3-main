using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    private TMP_Dropdown _dropdown;
    private PlayerCardConfigContainer _container;
    private PlayerAttacker _attacker;
    private ICardFactory _factory;

    private List<WeaponConfig> _configs = new List<WeaponConfig>();

    public void Init(PlayerCardConfigContainer container, ICardFactory factory, PlayerAttacker attacker)
    {
        _container = container;
        _factory = factory;
        _attacker = attacker;

        _container.WeaponAdded += OnWeaponAdded;
        _container.WeaponRemoved += OnWeaponRemoved;

        if (_attacker.CurrentWeapon != null)
        {
            OnWeaponAdded(_attacker.CurrentWeapon.Config);
        }
    }

    private void Awake()
    {
        _dropdown = GetComponentInChildren<TMP_Dropdown>();
        _dropdown.onValueChanged.AddListener(OnDropdownSelected);
    }

    private void OnDestroy()
    {
        _container.WeaponAdded -= OnWeaponAdded;
        _container.WeaponRemoved -= OnWeaponRemoved;

        _dropdown.onValueChanged.RemoveListener(OnDropdownSelected);
    }

    private void OnWeaponAdded(WeaponConfig config)
    {
        _configs.Add(config);
        _factory.ActivateCard(config);

        var option = new TMP_Dropdown.OptionData();
        option.image = config.Icon;

        _dropdown.options.Add(option);
        _dropdown.RefreshShownValue();
    }

    private void OnWeaponRemoved(WeaponConfig config)
    {
        int index = _configs.IndexOf(config);

        if (index >= 0)
        {
            _configs.RemoveAt(index);
            _dropdown.options.RemoveAt(index);
            _dropdown.RefreshShownValue();
        }
    }

    private void OnDropdownSelected(int index)
    {
        var selectedConfig = _configs[index];
        _attacker.SetWeapon(selectedConfig);
    }
}