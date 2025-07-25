using System;
using UnityEngine;

[RequireComponent(typeof(DamageText))]

public class Health : MonoBehaviour, IDemageable
{
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private float _maxHealth = 1;
    [SerializeField] private float _minValue = 3f;
    [SerializeField] private float _maxValue = 15f;
    [SerializeField] private TargetType _targetType;

    private float _incomingDamage;
    private float _currentValue;

    public float IncomingDamage => _incomingDamage;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentValue;

    public event Action<float> IsValueChange;
    public event Action<float> HealthLost;
    public event Action<IDemageable> Died;

    public TargetType GetTargetType() => _targetType;

    private void Awake()
    {
        if (_playerConfig == null)
        {
            _maxHealth = (int)UnityEngine.Random.Range(_minValue, _maxValue);
        }
        else
        {
            _maxHealth = _playerConfig.MaxHealth;
        }

        _currentValue = _maxHealth;
    }

    public void Heal(float healAmount)
    {
        if (_currentValue > 0)
        {
            if (_currentValue + healAmount > _maxHealth)
            {
                _currentValue = _maxHealth;
            }
            else
            {
                _currentValue += healAmount;
            }

            IsValueChange?.Invoke(_currentValue);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_currentValue <= 0) return;

        float damageTaken = Mathf.Min(damage, _currentValue);
        _currentValue -= damageTaken;

        HealthLost?.Invoke(damageTaken);
        IsValueChange?.Invoke(_currentValue);

        if (_currentValue <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Died?.Invoke(this);
        Died = null;
        Destroy(gameObject);
    }
}
