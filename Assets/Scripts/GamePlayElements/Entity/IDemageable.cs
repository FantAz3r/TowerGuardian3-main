using System;
public interface IDemageable
{
    event Action<float, float> IsValueChange;
    event Action<float> DamageTaken;
    event Action<Health> Killed;
    event Action Died;

    HealthConfig Config { get; }
    float CurrentHealth { get; }
    float MaxHealth { get; }

    void TakeDamage(float damage);
}