using DG.Tweening;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class Stone : MonoBehaviour
    {
        private float _shakeDuration = 0.3f;
        private float _shakeStrength = 0.2f;
        private int _vibrato = 10;
        private float _randomness = 90f;
        private Health _health;
        private Tween _shakeTween;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.Died += Die;
            _health.IsValueChange += OnTakeDamage;
        }

        private void OnDisable()
        {
            _shakeTween?.Kill();
            _health.Died -= Die;
            _health.IsValueChange -= OnTakeDamage;
        }

        private void OnTakeDamage(float useles, float useles1)
        {
            _shakeTween = transform.DOShakePosition(_shakeDuration, _shakeStrength, _vibrato, _randomness);
        }

        private void Die()
        {
            _shakeTween?.Kill();
            _health.Die();
        }
    }
}
