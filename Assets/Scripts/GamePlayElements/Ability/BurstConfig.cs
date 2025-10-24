
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BurstAbilityConfig")]
public class BurstConfig : AbilityConfig
{
    [SerializeField] private int _baseHitCount = 4;
    [SerializeField] private float _baseAttackDelay = 0.3f;
    [SerializeField] private float _baseCooldown = 15f;

    [SerializeField] private int _hitCountPerLevel = 1;
    [SerializeField] private float _attackDelayReductionPerLevel = 0.02f;
    [SerializeField] private float _cooldownReductionPerLevel = 1f;

    [SerializeField] private float _minCooldown = 3f;
    [SerializeField] private float _minAttackDelay = 0.1f;

    public int HitCount => GetHitCount(Level);
    public float AttackDelay => GetAttackDelay(Level);
    public float Cooldown => GetCooldown(Level);

    public int GetHitCount(int level)
    {
        return _baseHitCount + _hitCountPerLevel * (level - 1);
    }

    public float GetAttackDelay(int level)
    {
        return Mathf.Max(_minAttackDelay, _baseAttackDelay - _attackDelayReductionPerLevel * (level - 1));
    }

    public float GetCooldown(int level)
    {
        return Mathf.Max(_minCooldown, _baseCooldown - _cooldownReductionPerLevel * (level - 1));
    }

    public override List<CardStats> GetStats()
    {
        int level = Level; 
        int nextLevel = level + 1;

        return new List<CardStats>
        {
            new CardStats("Hit Count", GetHitCount(level), GetHitCount(nextLevel)),
            new CardStats("Attack Delay", GetAttackDelay(level),  GetAttackDelay(nextLevel)),
            new CardStats("Cooldown", GetCooldown(level), GetCooldown(nextLevel)),
        };
    }
}
