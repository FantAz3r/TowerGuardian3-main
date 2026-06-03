using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/FireBallConfig")]
public class FireballConfig : AbilityConfig
{
    [SerializeField] private float _exploadDamage = 10f;
    [SerializeField] private float _maxFlyDistance = 15f;
    [SerializeField] private float _exploadRange = 5f;
    [SerializeField] private float _flySpeed = 20f;
    [SerializeField] private float _cooldown = 20f;
    [SerializeField] private float _minCooldown = 2.5f;

    [SerializeField] private float _cooldownPerLevel = 1f;
    [SerializeField] private float _damagePerLevel = 2f;
    [SerializeField] private float _distancePerLevel = 1f;

    [field: SerializeField] public Fireball FireballPrefab { get; private set; }
    public float ExploadDamage => Mathf.Max(_exploadDamage, _exploadDamage + _damagePerLevel * (Level - 1));
    public float MaxFlyDistance => Mathf.Max(_maxFlyDistance, _maxFlyDistance + _distancePerLevel * (Level - 1));
    public float Cooldown => Mathf.Max(_minCooldown, _cooldown - _cooldownPerLevel * (Level - 1));
    public float ExploadRange => _exploadRange; 
    public float FlySpeed => _flySpeed;        

    public override List<CardStats> GetStats()
    {
        return new List<CardStats>
        {
            new CardStats(UIText.Damage, ExploadDamage, _exploadDamage + _damagePerLevel * Level),
            new CardStats(UIText.FlightDistance, MaxFlyDistance, _maxFlyDistance + _distancePerLevel * Level),
        };
    }
}
