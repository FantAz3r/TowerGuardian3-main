using System;
public interface IDemageable
{
    float IncomingDamage { get; }
    float MaxHealth { get; }
    float CurrentHealth { get; }

    event Action<float> IsValueChange;
    event Action<float> HealthLost;
    event Action<Health> Died;

    void TakeDamage(float damage);
}

