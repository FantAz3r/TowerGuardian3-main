using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<float> OnKilled;
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

    private void OnEnemyDied(Health enemy)
    {
        OnKilled?.Invoke(enemy.Config.MaxHealth);

        switch (enemy.GetHealthType())
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
