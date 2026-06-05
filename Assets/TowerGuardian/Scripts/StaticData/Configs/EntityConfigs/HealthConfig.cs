using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Configs.EntityConfigs
{
    [CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/HealthConfig")]

    public class HealthConfig : ScriptableObject, IDemageableConfig
    {
        [field: SerializeField] public float MaxHealth { get; private set; } = 1f;
        [field: SerializeField] public float DamageToErn { get; private set; }
        [field: SerializeField] public int RewardCount { get; private set; }
        [field: SerializeField] public int ScorePoints { get; private set; }
        [field: SerializeField] public ResourceType SpawnResource { get; private set; }
        [field: SerializeField] public EffectType SpawnEffect { get; private set; }

        [field: SerializeField] public int Level { get; private set; }

        [SerializeField] private float _healthGrowthPerLevel = 3f;

        public void SetLevel(int level)
        {
            Level = Mathf.Max(level, 0);
        }

        public float GetMaxHealth()
        {
            return Mathf.RoundToInt(MaxHealth * Mathf.Pow(_healthGrowthPerLevel, Level));
        }
    }
}