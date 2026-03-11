using System;
public interface IDemageable
{
    HealthConfig Config { get; }
    float CurrentHealth { get; }
    float MaxHealth { get; }

    event Action<float,float> IsValueChange;
    event Action<float> DamageTaken;
    event Action<Health> Killed;
    event Action Died;

    void TakeDamage(float damage);
}

