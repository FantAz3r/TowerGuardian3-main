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
    }

    public void Init(ISpawnerService spawnerService)
    {
        _spawnerService = spawnerService;
    }

    private void OnEnable()
    {
        _health.Healed += OnHeal;
        _health.DamageTaken += OnTakeDamage;
        _health.Died += OnDie;
    }

    private void OnHeal(float value)
    {
        _spawnerService.SendReqest(SpawnerType.Text, _health.Config, transform.position, (int)value);
    }

    private void OnTakeDamage(float value)
    {
        _spawnerService.SendReqest(SpawnerType.Text, _health.Config, transform.position, (int)value);
        _spawnerService.SendReqest(SpawnerType.Effects, _health.Config, transform.position);

        if (_health.Config.DamageToErn == 0)
            return;

        int spawnCount = 0;
        _damageAccumulator += value;

        if(_damageAccumulator >= _health.Config.DamageToErn)
        {
            int rewardsCount = (int)(_damageAccumulator / _health.Config.DamageToErn);
            spawnCount = rewardsCount * _health.Config.RewardCount;
            _spawnerService.SendReqest(SpawnerType.Resources, _health.Config, transform.position, spawnCount);
            _damageAccumulator -= rewardsCount * _health.Config.DamageToErn;
        }
    }

    private void OnDie(Health useles)
    {
        if (_health.Config.DamageToErn == 0)
            return;

        _spawnerService.SendReqest(SpawnerType.Resources, _health.Config, transform.position, _health.Config.RewardCount);
    }
}
