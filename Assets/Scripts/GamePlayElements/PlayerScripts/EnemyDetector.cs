using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private IScoreService _service;

    private List<Health> _targets = new();
    public IReadOnlyList<Health> Targets => _targets;

    public event Action<float> OnGetExperience;
    public event Action OnEnemyKilled;
    public event Action OnBossKilled;

    private void Awake()
    {
        _service = ServiceLocator.Get<IScoreService>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health enemy) && enemy != null)
        {
            enemy.Killed += OnEnemyDied;
            _targets.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Health enemy) && enemy != null)
        {
            enemy.Killed -= OnEnemyDied;
            _targets.Remove(enemy);
        }
    }

    private void OnEnemyDied(Health target)
    {

        target.Killed -= OnEnemyDied;
        _targets.Remove(target);
        _service.AddScore(ScoreType.Kill, target.Config.ScorePoints);


        if (target.GetHealthType() == EntityType.Enemy)
        {
            OnGetExperience?.Invoke(target.MaxHealth);
        }
        else
        {
            OnGetExperience?.Invoke(target.MaxHealth / 2);
        }

        switch (target.GetHealthType())
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
