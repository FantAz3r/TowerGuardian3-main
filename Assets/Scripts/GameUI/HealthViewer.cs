using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthViewer : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private float _smoothSpeed = 0.5f; 

    private Health _health;
    private Coroutine _smoothChangeCoroutine;
    private bool _iaActive = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        

        _health.MaxHealthChanged += OnMaxHealthChanged;
        _health.IsValueChange += OnHealthChanged;

        _healthSlider.gameObject.SetActive(_iaActive);
    }

    private void OnDisable()
    {
        _health.IsValueChange -= OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        if(_iaActive == false)
            _healthSlider.gameObject.SetActive(true);

        if (_smoothChangeCoroutine != null)
            StopCoroutine(_smoothChangeCoroutine);

        _smoothChangeCoroutine = StartCoroutine(SmoothHealthChange(currentHealth));
    }

    private IEnumerator SmoothHealthChange(float targetValue)
    {
        float startValue = _healthSlider.value;
        float elapsed = 0f;

        while (elapsed < _smoothSpeed)
        {
            elapsed += Time.deltaTime;
            _healthSlider.value = Mathf.Lerp(startValue, targetValue, elapsed / _smoothSpeed);
            yield return null;
        }

        _healthSlider.value = targetValue;
    }

    private void OnMaxHealthChanged(float currentHealth, float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = currentHealth;
    }
}

