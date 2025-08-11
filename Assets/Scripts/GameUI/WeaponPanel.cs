using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPanel : MonoBehaviour
{
    private TMP_Dropdown _dropdown; 
    private PlayerCardConfigContainer _container;
    private ICardFactory _factory;

    private List<WeaponConfig> _configs = new List<WeaponConfig>();

    public void Init(PlayerCardConfigContainer container, ICardFactory factory)
    {
        _container = container;
        _factory = factory;
        _dropdown = GetComponentInChildren<TMP_Dropdown>();
        _dropdown.onValueChanged.AddListener(OnDropdownSelected);
        _container.WeaponAdded += OnWeaponAdded;
    }

    private void OnDestroy()
    {
        if (_container != null)
            _container.WeaponAdded -= OnWeaponAdded;

        if (_dropdown != null)
            _dropdown.onValueChanged.RemoveListener(OnDropdownSelected);
    }

    private void OnWeaponAdded(WeaponConfig config)
    {
        _configs.Add(config);

        var option = new TMP_Dropdown.OptionData();
        option.image = config.Icon;

        _dropdown.options.Add(option);

        if (_configs.Count == 1)
            _dropdown.value = 0;

        _dropdown.RefreshShownValue();
    }

    private void OnDropdownSelected(int index)
    {
        if (index < 0 || index >= _configs.Count)
            return;

        var selectedConfig = _configs[index];
        _factory?.ActivateCard(selectedConfig);
    }
}