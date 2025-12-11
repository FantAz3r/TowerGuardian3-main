using UnityEngine;

public abstract class HealthConfig : ScriptableObject, IDemageableConfig
{
    [SerializeField] private float _maxHealth = 1f;
    [SerializeField] private float _damageToErn;
    [SerializeField] private int _rewardCount;
    [SerializeField] private ResourceType _spawnResource;
    [SerializeField] private EffectType _spawnEffect;

    public float MaxHealth => _maxHealth;
    public float DamageToErn => _damageToErn;
    public int RewardCount => _rewardCount;
    public ResourceType SpawnResource => _spawnResource;
    public EffectType SpawnEffect => _spawnEffect;
}