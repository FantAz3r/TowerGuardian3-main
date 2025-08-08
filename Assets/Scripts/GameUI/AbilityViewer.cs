using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityViewer : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _cooldownFillImage;
    [SerializeField] private TMP_Text _cooldownText;

    private Button _button;
    private IAbility _ability;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
        _cooldownText.enabled = false;
        DeactivateViewer();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
        UnsubscribeCooldownEvents();
    }

    public void ActivateViewer(IAbility ability, AbilityConfig config)
    {
        if (_iconImage == null && config == null)
            throw new ArgumentNullException();

        _ability = ability;
        gameObject.SetActive(true);
        _button.interactable = true;
        _iconImage.sprite = config.Icon;
        _cooldownFillImage.fillAmount = 0f;

        SubscribeCooldownEvents();
    }

    public void DeactivateViewer()
    {
        UnsubscribeCooldownEvents();

        _ability = null;
        gameObject.SetActive(false);
        _button.interactable = false;
        _cooldownFillImage.fillAmount = 0f;
    }

    private void OnClick()
    {
        if (_ability != null)
        {
            _ability.Use();
        }
    }

    private void SubscribeCooldownEvents()
    {
        if (_ability is ICooldownAbility ability)
        {
            _cooldownText.enabled = true;
            _cooldownText.text = ability.Cooldown.ToString();
            ability.CooldownStarted += CooldownView;
        }
    }

    private void UnsubscribeCooldownEvents()
    {
        if (_ability is ICooldownAbility ability)
        {
            _cooldownText.enabled = false;
            ability.CooldownStarted -= CooldownView;
        }
    }

    private void CooldownView(float cooldown, float passTime)
    {
        _cooldownFillImage.fillAmount = passTime / cooldown;
        _cooldownFillImage.fillAmount = 0f;
    }
}
