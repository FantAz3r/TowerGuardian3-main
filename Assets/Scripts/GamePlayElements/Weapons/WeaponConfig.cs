using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]

public class WeaponConfig : CardConfig
{
    [SerializeField] private Weapon _prefab;
    [SerializeField] private WeaponType _type;
    [SerializeField] private EntityType _targetType;
    [SerializeField] private RuntimeAnimatorController _controller;

    [SerializeField] private float _baseDamage = 10f;
    [SerializeField] private float _baseAttackDelay = 1f;
    [SerializeField] private float _baseAttackRange = 2f;
    [SerializeField] private float _baseMultiply = 2f;

    [SerializeField] private float _damageUpgradePercent = 0.25f;        
    [SerializeField] private float _attackDelayUpgradeFactor = -0.05f;   
    [SerializeField] private float _attackRangeUpgradeValue = 0.2f;      
    [SerializeField] private float _multiplyUpgradeValue = 0.1f;
    
    public Weapon Prefab => _prefab;
    public EntityType TargetType => _targetType;
    public WeaponType WeaponType => _type;
    public RuntimeAnimatorController Controller => _controller;
    public float Damage => GetDamage(Level);
    public float AttackDelay => GetAttackDelay(Level);
    public float AttackRange => GetAttackRange(Level);
    public float Multiply => GetMultiply(Level);

    public override CardType GetCardType() => CardType.WeaponSetter;

    public float GetDamage(int level)
    {
        return _baseDamage * Mathf.Pow(1 + _damageUpgradePercent, level - 1);
    }

    public float GetAttackDelay(int level)
    {
        return Mathf.Max(0.1f, _baseAttackDelay * Mathf.Pow(1 + _attackDelayUpgradeFactor, level - 1));
    }

    public float GetAttackRange(int level)
    {
        return _baseAttackRange + _attackRangeUpgradeValue * (level - 1);
    }

    public float GetMultiply(int level)
    {
        return _baseMultiply + _multiplyUpgradeValue * (level - 1);
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
