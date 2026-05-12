using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDemageable, IHealable, ITransfomable, IBuffble
{
    [SerializeField] private HealthConfig _config;
    [SerializeField] private EntityType _type;

    private StatsCalculator _statsCalculator;
    private float _currentValue, _startMaxHealth, _maxHealth;

    public HealthConfig Config => _config;
    public float CurrentHealth => _currentValue;
    public float MaxHealth => _maxHealth;
    public bool IsAlive { get; private set; } = true;
    public bool IsImmunity { get; private set; } = false;

    public event Action<float, float> IsValueChange, MaxHealthChanged;
    public event Action<float> DamageTaken, Healed;
    public event Action<Health> Killed;

    public event Action Died,
        Destroyed,
        Resurected,
        ImmunityObjectHited,
        ImmunityActivated,
        ImmunityDisabled;

    public Transform GetTransform() => transform;
    public EntityType GetHealthType() => _type;

    protected virtual void Awake()
    {
        _statsCalculator = new StatsCalculator();
        _maxHealth = _config.MaxHealth;
        _startMaxHealth = _config.MaxHealth;
    }

    public void Init(float maxHealth)
    {
        IsAlive = true;
        _maxHealth = maxHealth;
        _startMaxHealth = maxHealth;
        _currentValue = _maxHealth;
        MaxHealthChanged?.Invoke(_currentValue, _maxHealth);
    }

    public void OnEnable()
    {
        IsAlive = true;
        _currentValue = _maxHealth;
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    public void Heal(float healAmount)
    {
        if (IsAlive == false) return;
        
        if (_currentValue >= 0 && healAmount > 0)
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

    public virtual void TakeDamage(float damage)
    {
        if (IsAlive == false) { return; }

        if (IsImmunity)
        {
            ImmunityObjectHited?.Invoke();
            return;
        }

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

    public void ImmunityEnable()
    {
        IsImmunity = true;
        ImmunityActivated?.Invoke();
    }

    public void ImmunityDisable()
    {
        IsImmunity = false;
        ImmunityDisabled.Invoke();
    }

    public void ImmunityPerTime(float time)
    {
        StartCoroutine(ImmunityRourine(time));
    }

    private IEnumerator ImmunityRourine(float time)
    {
        ImmunityEnable();
        yield return new WaitForSeconds(time);
        ImmunityDisable();
    }

    public void ApplyBuff(IEffect effect)
    {
        _statsCalculator.AddEffect(effect);
        UpdateHealth();
    }

    public void Recalculate()
    {
        UpdateHealth();
    }

    public void RemoveBuff(IEffect effect)
    {
        _statsCalculator.RemoveEffect(effect);
        UpdateHealth();
    }

    public void DieAction()
    {
        IsAlive = false;
        Died?.Invoke();
        Killed?.Invoke(this);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void Resurect()
    {
        IsAlive = true;
        Heal(MaxHealth);
        Resurected?.Invoke();
    }

    public void EnableBuff()
    {
    }

    private void UpdateHealth()
    {
        float scaleRatio = _currentValue / _maxHealth;
        _maxHealth = _statsCalculator.Calculate(_startMaxHealth);
        _currentValue = _maxHealth * scaleRatio;
        IsValueChange?.Invoke(_currentValue, _maxHealth);
    }
}