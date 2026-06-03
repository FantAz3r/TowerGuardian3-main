using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/RotatingShuricenConfig")]
public class RotatingShurikenConfig : AbilityConfig
{
    [SerializeField] private Shuriken _shurikenPrefab;
    [SerializeField] private int _baseCount = 1;
    [SerializeField] private float _baseRadius = 3f;
    [SerializeField] private float _baseRotationSpeed = 90f;
    [SerializeField] private float _spinSpeed = 360f;
    [SerializeField] private float _baseDamage = 10;

    [SerializeField] private int _countPerLevel = 1;
    [SerializeField] private float _radiusPerLevel = 0.1f;
    [SerializeField] private float _rotationSpeedPerLevel = 5f;
    [SerializeField] private float _damagePerLevel = 2f;

    [SerializeField] private int _maxCount = 6;
    [SerializeField] private float _minRadius = 1f;
    [SerializeField] private float _minRotationSpeed = 30f;
    [SerializeField] private float _minDamage = 1f;

    public Shuriken ShuricrnPrefab => _shurikenPrefab;
    public int Count => Mathf.Min(_maxCount, _baseCount + _countPerLevel * (Level - 1));
    public float Radius => Mathf.Max(_minRadius, _baseRadius + _radiusPerLevel * (Level - 1));
    public float RotationSpeed => Mathf.Max(_minRotationSpeed, _baseRotationSpeed + _rotationSpeedPerLevel * (Level - 1));
    public int Damage => (int)Mathf.Max(_minDamage, _baseDamage + _damagePerLevel * (Level - 1));
    public float SpinSpeed => _spinSpeed;

    public override List<CardStats> GetStats()
    {
        return new List<CardStats>
        {
            new CardStats(UIText.Count, Count, Mathf.Min(_maxCount, _baseCount + _countPerLevel * Level)),
            new CardStats(UIText.Radius, Radius, Mathf.Max(_minRadius, _baseRadius + _radiusPerLevel * Level)),
            new CardStats(UIText.RotationSpeed, RotationSpeed, Mathf.Max(_minRotationSpeed, _baseRotationSpeed + _rotationSpeedPerLevel * Level)),
            new CardStats(UIText.Damage, Damage, Mathf.Max(_minDamage, _baseDamage + _damagePerLevel * Level)),
        };
    }
}