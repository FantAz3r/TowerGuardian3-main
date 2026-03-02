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
    [SerializeField] private Button _button;

    private PlayerAttacker _attacker;

    public IAbility Ability { get; private set; }
    public AbilityKeyCode AbilityKey { get; private set; }
    public bool HasAbility { get; private set; }

    private void Awake()
    {
        HasAbility = false;
        _button.onClick.AddListener(OnClick);
        _lockImage.gameObject.SetActive(false);
        _cooldownText.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
        UnsubscribeEvents();
    }

    public void ActivateViewer(IAbility ability, AbilityKeyCode keyCode, PlayerAttacker attacker)
    {
        HasAbility = true;
        Ability = ability;
        _attacker = attacker;
        _iconImage.sprite = ability.Config.Icon;
        _cooldownFillImage.fillAmount = 0f;
        gameObject.SetActive(true);
        SetKeyCode(keyCode);
        
        _attacker.WeaponSeted += UpdateLock;
        _attacker.WeaponActivated += UpdateLock;
        _attacker.WeaponDeactivated += UpdateLock;
        UpdateLock();
        SubscribeCooldownEvents();
    }

    public void DeactivateViewer()
    {
        UnsubscribeEvents();
    
        HasAbility = false;
        Ability = null;
        gameObject.SetActive(false);
        _button.interactable = false;
        _cooldownFillImage.fillAmount = 0f;
    }

    public void ActivateAbility()
    {
        if (Ability is UsebleAbility usebleAbility)
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
        if(Ability is UsebleAbility usebleAbility)
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
        if (Ability is ICooldownAbility ability)
        {
            _cooldownText.enabled = true;
            _cooldownText.text = ability.Cooldown.ToString("F1");
            ability.Cooldowning += CooldownView;
        }
    }

    private void UnsubscribeEvents()
    {
        if(_attacker != null)
        {
            _attacker.WeaponSeted -= UpdateLock;
            _attacker.WeaponActivated -= UpdateLock;
            _attacker.WeaponDeactivated -= UpdateLock;
        }

        if (Ability is ICooldownAbility ability)
        {
            _cooldownText.enabled = false;
            ability.Cooldowning -= CooldownView;
        }
    }

    private void CooldownView(float cooldown, float passTime)
    {
        _cooldownText.text = (cooldown - passTime).ToString("F1");
        _cooldownFillImage.fillAmount = 1 - (passTime / cooldown);

        if (_cooldownFillImage.fillAmount == 1)
        {
            _cooldownFillImage.fillAmount = 0;
        }
    }

    private void SetKeyCode(AbilityKeyCode keyCode)
    {
        if (keyCode != AbilityKeyCode.None)
        {
            AbilityKey = keyCode;
            _button.interactable = true;
            _keyCode.gameObject.SetActive(true);
            _keyCode.text = ((int)AbilityKey).ToString();
        }
        else
        {
            _keyCode.gameObject.SetActive(false);
        }
    }
}
