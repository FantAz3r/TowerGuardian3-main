using DG.Tweening;
using System;
using UnityEngine;

public class EnemyBuilding : MonoBehaviour
{
    [field: SerializeField] public Health Health { get; private set; }

    [SerializeField] private float _shakeDuration = 0.3f;
    [SerializeField] private float _shakeStrength = 0.2f;
    [SerializeField] private int _vibrato = 10;          
    [SerializeField] private float _randomness = 90f;    

    private Tween _shakeTween;

    public event Action Destroyed;

    private void Start()
    {
        Health.enabled = false;
        Health.IsValueChange += OnTakeDamage;
        Health.Died += OnDied;
    }

    private void OnTakeDamage(float oldValue, float newValue)
    {
        transform.DOShakePosition(_shakeDuration, _shakeStrength, _vibrato, _randomness);
    }

    private void OnDied()
    {
        Destroyed?.Invoke();

        Health.IsValueChange -= OnTakeDamage;
        Health.Died -= OnDied;

        if (_shakeTween != null)
            _shakeTween.Kill();

        Destroy(gameObject);
    }
}
