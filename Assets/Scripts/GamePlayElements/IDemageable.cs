using System;
public interface IDemageable
{
    float IncomingDamage { get; }
    float MaxHealth { get; }
    float CurrentHealth { get; }

    event Action<float> IsValueChange;
    event Action<float> HealthLost;
    event Action<IDemageable> Died;

    TargetType GetTargetType();

    void TakeDamage(float damage);
    void Heal(float healAmount);
}

