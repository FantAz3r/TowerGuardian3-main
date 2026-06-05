using System.Collections.Generic;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.CardsInfrastructure;
using TowerGuardian.Scripts.GamePlayElements.Weapons;
using TowerGuardian.Scripts.Localization;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Configs
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]

    public class WeaponConfig : CardConfig
    {
        [SerializeField] private float _damageUpgradePercent = 0.25f;
        [SerializeField] private float _attackDelayUpgradeFactor = -0.05f;
        [SerializeField] private float _attackRangeUpgradeValue = 0.2f;
        [SerializeField] private float _multiplyUpgradeValue = 0.1f;
        [SerializeField] private AudioClip _audioClip;

        [field: SerializeField] public Weapon Prefab { get; private set; }
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        [field: SerializeField] public EntityType TargetType { get; private set; }
        [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }
        [field: SerializeField] public AudioClip HitSound { get; private set; }
        [field: SerializeField] public bool IsAreaDamage { get; private set; }
        [field: SerializeField] public float BaseDamage { get; private set; } = 10f;
        [field: SerializeField] public float BaseAttackDelay { get; private set; } = 1f;
        [field: SerializeField] public float BaseAttackRange { get; private set; } = 2f;
        [field: SerializeField] public float BaseMultiply { get; private set; } = 2f;


        public float Damage => GetDamage(Level);
        public float AttackDelay => GetAttackDelay(Level);
        public float AttackRange => GetAttackRange(Level);
        public float Multiply => GetMultiply(Level);

        public override CardType GetCardType() => CardType.Weapon;

        public float GetDamage(int level)
        {
            return BaseDamage * Mathf.Pow(1 + _damageUpgradePercent, level - 1);
        }

        public float GetAttackDelay(int level)
        {
            return Mathf.Max(0.1f, BaseAttackDelay * Mathf.Pow(1 + _attackDelayUpgradeFactor, level - 1));
        }

        public float GetAttackRange(int level)
        {
            return BaseAttackRange + (_attackRangeUpgradeValue * (level - 1));
        }

        public float GetMultiply(int level)
        {
            return BaseMultiply + (_multiplyUpgradeValue * (level - 1));
        }

        public override List<CardStats> GetStats()
        {
            int level = Level;
            int nextLevel = level + 1;

            return new List<CardStats>
        {
            new CardStats(UIText.Damage, GetDamage(level), GetDamage(nextLevel)),
            new CardStats(UIText.AttackDelay, GetAttackDelay(level), GetAttackDelay(nextLevel)),
            new CardStats(UIText.AttackRange, GetAttackRange(level), GetAttackRange(nextLevel)),
        };
        }
    }
}