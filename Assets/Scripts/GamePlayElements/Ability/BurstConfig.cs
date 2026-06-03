using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BurstAbilityConfig")]
public class BurstConfig : AbilityConfig
{
    [SerializeField] private int _baseHitCount = 3;
    [SerializeField] private float _baseAttackDelay = 0.3f;
    [SerializeField] private float _baseCooldown = 15f;

    [SerializeField] private float _hitCountPerLevel = 0.2f;
    [SerializeField] private float _attackDelayReductionPerLevel = 0.01f;
    [SerializeField] private float _cooldownReductionPerLevel = 0.25f;

    [SerializeField] private float _minCooldown = 6f;
    [SerializeField] private float _minAttackDelay = 0.1f;

    public int HitCount => GetHitCount(Level);
    public float AttackDelay => GetAttackDelay(Level);
    public float Cooldown => GetCooldown(Level);

    public int GetHitCount(int level)
    {
        return (int)(_baseHitCount + (level - 1) * _hitCountPerLevel);
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
            new CardStats(UIText.HitCount, GetHitCount(level), GetHitCount(nextLevel)),
            new CardStats(UIText.AttackDelay, GetAttackDelay(level),  GetAttackDelay(nextLevel)),
            new CardStats(UIText.Cooldown, GetCooldown(level), GetCooldown(nextLevel)),
        };
    }
}
