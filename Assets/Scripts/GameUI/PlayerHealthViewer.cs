using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthViewer : MonoBehaviour
{
    [SerializeField] private Slider _healthImage;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private float _smoothSpeed = 2f;

    private Health _health;
    private Coroutine _lerpCoroutine;

    public void Init(Health health)
    {
        _health = health;
        _healthImage.value = 1f;
        _healthText.text = $"{_health.CurrentHealth} / {_health.MaxHealth}";
        _health.IsValueChange += View;
    }

    private void OnDestroy()
    {
        _health.IsValueChange -= View;

        if (_lerpCoroutine != null)
            StopCoroutine(_lerpCoroutine);
    }

    private void View(float currentHealth, float maxHealth)
    {
        _healthText.text = $"{currentHealth:F0} / {maxHealth:F0}";

        if (_lerpCoroutine != null)
            StopCoroutine(_lerpCoroutine);

        _lerpCoroutine = StartCoroutine(LerpHealthBar(_healthImage.value, (float) currentHealth / maxHealth, _smoothSpeed));
    }

    private IEnumerator LerpHealthBar(float startValue, float targetValue, float speed)
    {
        float currentValue = startValue;

        while (Mathf.Approximately(currentValue, targetValue) == false)
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, speed * Time.deltaTime);
            _healthImage.value = currentValue;
            yield return null;
        }

        _healthImage.value = targetValue;
        _lerpCoroutine = null;
    }
}

