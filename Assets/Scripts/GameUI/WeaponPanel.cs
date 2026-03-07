using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [field: SerializeField] public Highlighter Highlighter { get; private set; }
    private Player _player;
    private List<WeaponConfig> _configs = new List<WeaponConfig>();

    public event Action WeaponSwaped;
    public event Action WeaponAdded;

    private void Awake()
    {
        _player = ServiceLocator.Get<IGameFactory>().Player;

        gameObject.SetActive(true);

        if (_player.Attacker.CurrentWeapon != null)
        {
            OnWeaponAdded(_player.Attacker.CurrentWeapon.Config);
        }

        _player.CardHolder.CardAdded += OnWeaponAdded;
        _player.CardHolder.CardRemoved += OnWeaponRemoved;
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
        _player.CardHolder.CardRemoved -= OnWeaponRemoved;
    }

    private void OnWeaponAdded(ICardConfig card)
    {
        if (card is WeaponConfig weapon)
        {
            if (_configs.Contains(weapon))
                return;

            _configs.Add(weapon);

            var option = new TMP_Dropdown.OptionData();
            option.image = weapon.Icon;

            _dropdown.options.Add(option);
            WeaponAdded?.Invoke();

            OnWeaponUpgraded(card);
            card.Upgraded += OnWeaponUpgraded;
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
                card.Upgraded -= OnWeaponUpgraded;
                _configs.RemoveAt(index);
                _dropdown.options.RemoveAt(index);
                _dropdown.RefreshShownValue();

                int currentIndex = _dropdown.value;

                if (index == currentIndex)
                {
                    if (_configs.Count > 0)
                    {
                        int newIndex = Mathf.Clamp(index, 0, _configs.Count - 1);
                        _dropdown.value = newIndex;

                        _player.Attacker.SetWeapon(_configs[newIndex]);
                        WeaponSwaped?.Invoke();
                    }
                    else
                    {
                        WeaponSwaped?.Invoke();
                    }
                }
                else if (index < currentIndex)
                {
                    _dropdown.value = currentIndex - 1;
                }
            }
        }
    }

    private void OnWeaponUpgraded(ICardConfig config)
    {
        int index = _configs.IndexOf(config as WeaponConfig);
        if (index < 0)
            return;

        _dropdown.options[index].text = config.Level.ToString();
        _dropdown.RefreshShownValue();
        WeaponAdded?.Invoke();

        if (_dropdown.value == index)
        {
            _dropdown.captionText.text = config.Level.ToString();
        }
    }


    private void OnDropdownSelected(int index)
    {
        var selectedConfig = _configs[index];
        _player.Attacker.SetWeapon(selectedConfig);
        WeaponSwaped?.Invoke();
    }
}