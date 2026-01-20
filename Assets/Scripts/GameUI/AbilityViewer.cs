using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityViewer : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _cooldownFillImage;
    [SerializeField] private Image _lockImage;
    [SerializeField] private TMP_Text _cooldownText;
    [SerializeField] private TMP_Text _keyCode;

    private Button _button;
    private IAbility _ability;
    private PlayerAttacker _attacker;

    public AbilityKeyCode AbilityKey { get; private set; }
    public bool HasAbility { get; private set; }

    public IAbility Ability => _ability;

    private void Awake()
    {
        HasAbility = false;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _lockImage.gameObject.SetActive(false);
        _cooldownText.enabled = false;
        DeactivateViewer();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
        UnsubscribeCooldownEvents();
    }

    public void ActivateViewer(IAbility ability, AbilityConfig config, AbilityKeyCode keyCode, PlayerAttacker attacker)
    {
        if (_iconImage == null && config == null)
            throw new ArgumentNullException();

        HasAbility = true;
        _attacker = attacker;
        _ability = ability;

        gameObject.SetActive(true);
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        _button.interactable = true;
        _iconImage.sprite = config.Icon;
        _cooldownFillImage.fillAmount = 0f;

        if (keyCode != AbilityKeyCode.None)
        {
            AbilityKey = keyCode;
            _keyCode.text = ((int)AbilityKey).ToString();
        }
        else
        {
            _keyCode.gameObject.SetActive(false);
        }

        _attacker.WeaponSeted += UpdateLock;
        _attacker.WeaponActivated += UpdateLock;
        _attacker.WeaponDeactivated += UpdateLock;
        UpdateLock();
        SubscribeCooldownEvents();
    }

    public void DeactivateViewer()
    {
        UnsubscribeCooldownEvents();

        HasAbility = false;
        _ability = null;
        gameObject.SetActive(false);
        _button.interactable = false;
        _cooldownFillImage.fillAmount = 0f;
    }

    public void ActivateAbility()
    {
        if (_ability is UsebleAbility usebleAbility)
        {
            usebleAbility.Use();
        }
    }

    private void UpdateLock(IWeapon useles = null)
    {
        UpdateLock();
    }

    private void UpdateLock()
    {
        if(_ability is UsebleAbility usebleAbility)
        {
            _lockImage.gameObject.SetActive(usebleAbility.IsLock);
        }
    }

    private void OnClick()
    {
        ActivateAbility();
    }

    private void SubscribeCooldownEvents()
    {
        if (_ability is ICooldownAbility ability)
        {
            _cooldownText.enabled = true;
            ability.Cooldowning += CooldownView;
        }
    }

    private void UnsubscribeCooldownEvents()
    {
        if (_ability is ICooldownAbility ability)
        {
            _cooldownText.enabled = false;
            ability.Cooldowning -= CooldownView;
        }
    }

    private void CooldownView(float cooldown, float passTime)
    {
        _cooldownText.text = (cooldown - passTime).ToString();
        _cooldownFillImage.fillAmount = passTime / cooldown;
    }
}
