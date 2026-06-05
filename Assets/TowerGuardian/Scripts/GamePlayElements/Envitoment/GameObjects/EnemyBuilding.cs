using System;
using DG.Tweening;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class EnemyBuilding : MonoBehaviour
    {
        [SerializeField] private float _shakeDuration = 0.3f;
        [SerializeField] private float _shakeStrength = 0.2f;
        [SerializeField] private int _vibrato = 10;
        [SerializeField] private float _randomness = 90f;

        private Tween _shakeTween;

        public event Action Destroyed;

        [field: SerializeField] public Health Health { get; private set; }

        private void Start()
        {
            Health.enabled = false;
            Health.IsValueChange += OnTakeDamage;
            Health.Died += OnDied;
        }

        private void OnDisable()
        {
            OnDied();
        }

        private void OnTakeDamage(float oldValue, float newValue)
        {
            _shakeTween = transform.DOShakePosition(_shakeDuration, _shakeStrength, _vibrato, _randomness);
        }

        private void OnDied()
        {
            Destroyed?.Invoke();
            _shakeTween?.Kill();

            Health.IsValueChange -= OnTakeDamage;
            Health.Died -= OnDied;

            Destroy(gameObject);
        }
    }
}
