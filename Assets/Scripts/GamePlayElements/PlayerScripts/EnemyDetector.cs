using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private IScoreService _service;
    public event Action<float> OnGetExperience;
    public event Action OnEnemyKilled;
    public event Action OnBossKilled;
    private void Awake()
    {
        _service = ServiceLocator.Get<IScoreService>();
    }
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
        if (health.TryGetComponent(out IDemageable enemy))
        {
            enemy.Killed -= OnEnemyDied;
        }

        _service.AddScore(ScoreType.Kill, health.Config.ScorePoints);
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
