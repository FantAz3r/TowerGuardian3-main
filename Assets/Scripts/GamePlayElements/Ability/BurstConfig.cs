using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/BurstAbilityConfig")]
public class BurstConfig : AbilityConfig
{
    [SerializeField] private int _hitCount;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _cooldown;

    public float AttackDelay => _attackDelay;
    public float Cooldown => _cooldown;
    public int HitCount => _hitCount;

    public override Dictionary<string, float> GetStats()
    {
        return new Dictionary<string, float>
        {
            ["Hit Count"] = _hitCount,
            ["Attack Delay"] = _attackDelay,
            ["Cooldown"] = _cooldown
        };
    }
}