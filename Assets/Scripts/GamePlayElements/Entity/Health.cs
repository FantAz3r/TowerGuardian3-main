using System;
using UnityEngine;

public class Health : MonoBehaviour, IDemageable, IHealable, ITransfomable, IBuffble
{
    [SerializeField] private HealthConfig _config;
    [SerializeField] private EntityType _type;

    private float _currentValue;
    private float _startMaxHealth;
    private float _maxHealth;
    public HealthConfig Config => _config;
    public float CurrentHealth => _currentValue;
    public float MaxHealth => _maxHealth;

    public event Action<float,float> IsValueChange;
    public event Action<float> DamageTaken;
    public event Action<float> Healed;
    public event Action<Health> Killed;

    public event Action Died;
    public event Action Destroyed;

    public Transform GetTransform() => transform;
    public EntityType GetHealthType() => _type;

    private void Awake()
    {
        _maxHealth = _config.MaxHealth;
        _startMaxHealth = _config.MaxHealth;
    }

    public void OnEnable()
    {
        _currentValue = _maxHealth;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public void Heal(float healAmount)
    {
        if (_currentValue > 0 && healAmount > 0)
        {
            if (_currentValue + healAmount > _maxHealth)
            {
                healAmount = _maxHealth - _currentValue;
                _currentValue = _maxHealth;
            }
            else
            {
                _currentValue += healAmount;
            }

            Healed?.Invoke(healAmount);
            IsValueChange?.Invoke(_currentValue, _maxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        if (_currentValue <= 0) return;

        float damageTaken = Mathf.Min(damage, _currentValue);
        _currentValue -= damageTaken;
        DamageTaken?.Invoke(damageTaken);
        IsValueChange?.Invoke(_currentValue, _maxHealth);

        if (_currentValue < 1)
        {
            DieAction();
        }
    }

    public void ApplyBuff(float value)
    {
        float scaleRatio = _currentValue / _maxHealth;
        _maxHealth = _startMaxHealth * (value + 1);
        _currentValue = _maxHealth * scaleRatio;
        IsValueChange?.Invoke(_currentValue, _maxHealth);
    }

    public void RemoveBuff()
    {
        float scaleRatio = _currentValue / _maxHealth;
        _maxHealth = _startMaxHealth;
        _currentValue = _maxHealth * scaleRatio;
       IsValueChange?.Invoke(_currentValue, _maxHealth);
    }

    public void DieAction()
    {
        Died?.Invoke();
        Killed?.Invoke(this);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void EnableBuff()
    {
    }
}