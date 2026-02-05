using UnityEngine;

[RequireComponent(typeof(Health))]

public class SpawnbleEntity : MonoBehaviour
{
    private float _damageAccumulator = 0f;
    private Health _health;
    private ISpawnerService _spawnerService;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _spawnerService = ServiceLocator.Get<ISpawnerService>();

        _health.Healed += OnHeal;
        _health.DamageTaken += OnTakeDamage;
        _health.Died += OnDie;
    }

    private void OnDestroy()
    {
        _health.Healed -= OnHeal;
        _health.DamageTaken -= OnTakeDamage;
        _health.Died -= OnDie;
    }

    private void OnHeal(float value)
    {
        _spawnerService.SendTextReqest(transform.position, (int)value, Color.green);
    }

    private void OnTakeDamage(float value)
    {
        _spawnerService.SendTextReqest(transform.position, (int)value);
        _spawnerService.SendEffectReqest(_health.Config.SpawnEffect, transform.position);

        if (_health.Config.DamageToErn <= 0)
            return;

        int spawnCount = 0;
        _damageAccumulator += value;

        if(_damageAccumulator >= _health.Config.DamageToErn)
        {
            int rewardsCount = (int)(_damageAccumulator / _health.Config.DamageToErn);
            spawnCount = rewardsCount * _health.Config.RewardCount;
            _spawnerService.SendItemReqest(_health.Config, transform.position, spawnCount);
            _damageAccumulator -= rewardsCount * _health.Config.DamageToErn;
        }
    }

    private void OnDie()
    {
        if (_health.Config.DamageToErn > 0)
            return;

        _spawnerService.SendItemReqest(_health.Config, transform.position, _health.Config.RewardCount);
    }
}