using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/HealthConfig")]
public class HealthConfig : ScriptableObject, IDemageableConfig
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 1f;
    [field: SerializeField] public float DamageToErn { get; private set; }
    [field: SerializeField] public int RewardCount { get; private set; }
    [field: SerializeField] public ResourceType SpawnResource { get; private set; }
    [field: SerializeField] public EffectType SpawnEffect { get; private set; }
}