using DG.Tweening;
using TMPro;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;
using UnityEngine.UI;

namespace TowerGuardian.Scripts.UI.Elements
{
    public class BossHealthViewer : WindowBase
    {
        [SerializeField] private Slider _healthImage;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private float _smoothSpeed = 10f;

        private Tween _healthTween;
        private Health _health;

        public void Init(Health health)
        {
            _health = health;
            Open();
            _healthImage.value = _health.CurrentHealth / _health.MaxHealth;
            _healthText.text = $"{_health.CurrentHealth} / {_health.MaxHealth}";
            _health.IsValueChange += View;
            _health.Died += OnDied;
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                View(_health.CurrentHealth, _health.MaxHealth);
            }
        }

        private void OnDied()
        {
            _health.IsValueChange -= View;
        }

        private void OnDestroy()
        {
            _healthTween?.Kill();
            _health.Died -= OnDied;
        }

        private void View(float currentHealth, float maxHealth)
        {
            _healthText.text = $"{currentHealth:F0} / {maxHealth:F0}";

            float startValue = _healthImage.value;
            float targetValue = currentHealth / maxHealth;
            float duration = _smoothSpeed;

            HealthBarAnimation(startValue, targetValue, duration);
        }

        private void HealthBarAnimation(float startValue, float targetValue, float duration)
        {
            _healthTween?.Kill();
            _healthImage.value = startValue;
            _healthTween = DOTween.To(() => _healthImage.value, x => _healthImage.value = x, targetValue, duration)
                                 .SetEase(Ease.Linear)
                                 .OnComplete(() => _healthTween = null);
        }
    }
}