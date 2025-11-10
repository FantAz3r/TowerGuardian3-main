using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthViewer : MonoBehaviour
{
    [SerializeField] private Slider _healthImage;
    [SerializeField] private TMP_Text _healthText;

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

    private void View(float damage)
    {
        _healthText.text = $"{_health.CurrentHealth} / {_health.MaxHealth}";

        if (_lerpCoroutine != null)
            StopCoroutine(_lerpCoroutine);

        _lerpCoroutine = StartCoroutine(LerpHealthBar(_healthImage.value, (float)_health.CurrentHealth / _health.MaxHealth, 0.5f));
    }

    private IEnumerator LerpHealthBar(float startValue, float targetValue, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            _healthImage.value = Mathf.Lerp(startValue, targetValue, time / duration);
            yield return null;
        }

        _healthImage.value = targetValue;
    }
}

