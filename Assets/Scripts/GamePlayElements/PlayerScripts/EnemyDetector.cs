using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<float> OnGetExperience;
    public event Action<Health> OnKilled;
    public event Action OnEnemyKilled;
    public event Action OnBossKilled;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDemageable enemy) && enemy != null)
        {
            enemy.Killed += OnEnemyDied;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDemageable enemy) && enemy != null)
        {
            enemy.Killed -= OnEnemyDied;
        }
    }

    private void OnEnemyDied(Health health)
    {
        if (health.TryGetComponent<IDemageable>(out var enemy))
        {
            enemy.Killed -= OnEnemyDied;
        }

        OnGetExperience?.Invoke(enemy.Config.MaxHealth);

        switch (health.GetHealthType())
        {
            case EntityType.Enemy:
                OnEnemyKilled?.Invoke();
                break;

            case EntityType.Boss:
                OnBossKilled?.Invoke();
                break;

            default:
                break;
        }
    }
}
