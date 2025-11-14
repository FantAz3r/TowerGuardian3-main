using System;
using UnityEngine;

public class EnemyDetecter : MonoBehaviour
{

    public event Action<float> OnKilled;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDemageable enemy) && enemy != null)
        {
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDemageable enemy) && enemy != null)
        {
            enemy.Died -= OnEnemyDied;
        }
    }

    private void OnEnemyDied(IDemageable enemy)
    {
        OnKilled?.Invoke(enemy.MaxHealth);
    }
}
