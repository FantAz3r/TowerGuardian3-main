using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity
{
    [RequireComponent(typeof(Health))]

    public class SpawnbleEntity : MonoBehaviour
    {
        private float _damageAccumulator;
        private Health _health;
        private ISpawnerService _spawnerService;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _spawnerService = ServiceLocator.Get<ISpawnerService>();

            _health.Died += OnDie;
        }

        private void OnEnable()
        {
            _health.Healed += OnHeal;
            _health.DamageTaken += OnTakeDamage;
        }

        private void OnDisable()
        {
            _health.Healed -= OnHeal;
            _health.DamageTaken -= OnTakeDamage;
        }

        private void OnDestroy()
        {
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
            {
                return;
            }

            int spawnCount = 0;
            float damageToErn = _health.Config.DamageToErn * _health.Config.GetMaxHealth() / _health.Config.MaxHealth;
            _damageAccumulator += value;

            if (_damageAccumulator >= damageToErn)
            {
                int rewardsCount = (int)(_damageAccumulator / damageToErn);
                spawnCount = rewardsCount * CalculateRewardToKill();
                _spawnerService.SendItemReqest(_health.Config, transform.position, spawnCount);
                _damageAccumulator -= rewardsCount * damageToErn;
            }
        }

        private void OnDie()
        {
            if (_health.Config.DamageToErn > 0)
            {
                return;
            }

            _spawnerService.SendItemReqest(_health.Config, transform.position, CalculateRewardToKill());
        }

        private int CalculateRewardToKill()
        {
            return Mathf.CeilToInt(_health.Config.RewardCount * _health.Config.GetMaxHealth() / _health.Config.MaxHealth);
        }
    }
}