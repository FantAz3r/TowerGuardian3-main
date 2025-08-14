using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AxeThrowingConfig", menuName = "Abikities/AxeThrowingConfig")]
public class AxeThrowingConfig : AbilityConfig
{
    [SerializeField] private float _cooldown = 10f;
    [SerializeField] private float _flightDistance = 10f;
    [SerializeField] private float _flightDuration = 1.0f;
    [SerializeField] private float _ellipseHeight = 2.0f;
    [SerializeField] private float _damage = 7f;

    public float Cooldown=>_cooldown;
    public float FlightDistance => _flightDistance;
    public float FlightDuration => _flightDuration;
    public float EllipseHeight => _ellipseHeight;
    public float Damage => _damage;

    public override Dictionary<string, float> GetStats()
    {
        return new Dictionary<string, float>
        {
            ["Flight Distance"] = _flightDistance,
            ["Damage"] = _damage,
            ["Cooldown"] = _cooldown
        };
    }
}