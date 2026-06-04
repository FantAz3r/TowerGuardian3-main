using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PortalFrame : MonoBehaviour
{
    public event Action Disabled;

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public HealthViewer HealthViewre { get; private set; }
    [field: SerializeField] public Slider HealthViewSlider { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }
    public bool IsActive { get; private set; } = false;

    private void Awake()
    {
        Deactivate();
    }

    public void Activate()
    {
        HealthViewSlider.gameObject.SetActive(true);
        IsActive = true;
        Health.enabled = true;
        HealthViewre.enabled = true;
        Collider.enabled = true;    
        Health.IsValueChange += OnTakeDamage;
        Health.Died += OnDied;
    }

    public void Deactivate()
    {
        Health.IsValueChange -= OnTakeDamage;
        Health.Died -= OnDied;
        Disabled?.Invoke();

        Collider.enabled = false;
        Health.enabled = false;
        HealthViewre.enabled = false;
        IsActive = false;
        HealthViewSlider.gameObject.SetActive(false);
    }

    private void OnTakeDamage(float useles, float useles1)
    {
        float duration = 0.5f;
        float strength = 0.3f;
        int vibrato = 10;
        float randomness = 90f;

        transform.DOShakePosition(duration, strength, vibrato, randomness);
    }

    private void OnDied()
    {
        Deactivate();
        Destroy(this);
    }

    private void OnDisable()
    {
        Deactivate();
        Disabled?.Invoke();
    }

    private void OnDestroy()
    {
        Health.IsValueChange -= OnTakeDamage;
        Health.Died -= OnDied;
    }
}
