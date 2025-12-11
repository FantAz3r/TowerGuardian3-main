using System;
public interface IDemageable
{
    HealthConfig Config { get; }
    float CurrentHealth { get; }

    event Action<float> IsValueChange;
    event Action<float> DamageTaken;
    event Action<Health> Died;

    void TakeDamage(float damage);
}

