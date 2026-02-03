using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    private Player _player;
    private List<WeaponConfig> _configs = new List<WeaponConfig>();

    public void Init(Player player)
    {
        _player = player;

        gameObject.SetActive(true);

        if (_player.Attacker.CurrentWeapon != null)
        {
            OnWeaponAdded(_player.Attacker.CurrentWeapon.Config);
        }

        _player.CardHolder.CardAdded += OnWeaponAdded;
        _player.CardHolder.CardAdded += OnWeaponRemoved;
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownSelected);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnDropdownSelected);
    }

    private void OnDestroy()
    {
        _player.CardHolder.CardAdded -= OnWeaponAdded;
        _player.CardHolder.CardAdded -= OnWeaponRemoved;
    }

    private void OnWeaponAdded(ICardConfig card)
    {
        if(card is WeaponConfig weapon)
        {
            if (_configs.Contains(weapon))
                return;

            _configs.Add(weapon);

            var option = new TMP_Dropdown.OptionData();
            option.image = weapon.Icon;

            _dropdown.options.Add(option);
            _dropdown.RefreshShownValue();
        }
    }

    private void OnWeaponRemoved(ICardConfig card)
    {
        if (card is WeaponConfig weapon)
        {
            int index = _configs.IndexOf(weapon);

            if (index >= 0)
            {
                _configs.RemoveAt(index);
                _dropdown.options.RemoveAt(index);
                _dropdown.RefreshShownValue();
            }
        }
    }

    private void OnDropdownSelected(int index)
    {
        var selectedConfig = _configs[index];
        _player.Attacker.SetWeapon(selectedConfig);
    }
}