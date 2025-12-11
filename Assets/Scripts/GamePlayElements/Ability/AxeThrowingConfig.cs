using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AxeThrowingConfig", menuName = "Abilities/AxeThrowingConfig")]
public class AxeThrowingConfig : AbilityConfig
{
    [SerializeField] private float _baseCooldown = 10f;
    [SerializeField] private float _flightDuration = 2f;
    [SerializeField] private float _baseFlightDistance = 10f;
    [SerializeField] private float _baseDamage = 7f;

    [SerializeField] private float _cooldownPerLevel = -1f;
    [SerializeField] private float _flightDistancePerLevel = 0.5f;
    [SerializeField] private float _damagePerLevel = 1f;

    public float Cooldown => GetCooldown(Level);
    public float FlightDistance => GetFlightDistance(Level);
    public float FlightDuration => _flightDuration;
    public int Damage => (int)GetDamage(Level);

    public float GetCooldown(int level)
    {
        return Mathf.Max(1f, _baseCooldown + _cooldownPerLevel * (level - 1));
    }

    public float GetFlightDistance(int level)
    {
        return _baseFlightDistance + _flightDistancePerLevel * (level - 1);
    }

    public float GetDamage(int level)
    {
        return _baseDamage + _damagePerLevel * (level - 1);
    }

    public override List<CardStats> GetStats()
    {
        int level = Level;
        int nextLevel = Level + 1;

        return new List<CardStats>
        {
            new CardStats("Flight Distance", GetFlightDistance(level), GetFlightDistance(nextLevel)),
            new CardStats("Damage", GetDamage(level), GetDamage(nextLevel)),
            new CardStats("Cooldown", GetCooldown(level), GetCooldown(nextLevel))
        };
    }
}
