using System;
using UnityEngine;

public class Health : MonoBehaviour, IDemageable, IHealable, ITransfomable, IBuffble
{
    [SerializeField] private DemageableConfig _configObject;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minValue = 3f;
    [SerializeField] private float _maxValue = 15f;
    [SerializeField] private EntityType _type;

    private float _incomingDamage;
    private float _currentValue;

    public float IncomingDamage => _incomingDamage;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentValue;

    public event Action<float> IsValueChange;
    public event Action<float> HealthLost;
    public event Action<Health> Died;

    public Transform GetTransform() => transform;
    public EntityType GetHealthType() => _type;

    private void Awake()
    {
        if (_configObject == null)
        {
            _maxHealth = (int)UnityEngine.Random.Range(_minValue, _maxValue);
        }
        else
        {
            _maxHealth = _configObject.MaxHealth;
        }

        _currentValue = _maxHealth;
    }

    public void OnEnable()
    {
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

    public void ApplyBuff(float value)
    {
        _maxHealth = _maxHealth + _maxHealth * value;
        _currentValue = _currentValue + (_currentValue * value);
        IsValueChange?.Invoke(_currentValue);
    }

    private void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}
