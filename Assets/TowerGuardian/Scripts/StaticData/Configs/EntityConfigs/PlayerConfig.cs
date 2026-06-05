using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Configs.EntityConfigs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]

    public class PlayerConfig : ScriptableObject, ILevelConfig
    {
        [field: SerializeField] public MoveConfig MoveConfig { get; private set; }
        [field: SerializeField] public HealthConfig HealthConfig { get; private set; }

        [field: SerializeField] public float HealthRegeneration { get; private set; } = 1f;
        [field: SerializeField] public int InventoryCapacity { get; private set; } = 10000;
        [field: SerializeField] public float BaseLvlCost { get; private set; } = 100f;
        [field: SerializeField] public float LevelCostMultiplier { get; private set; } = 1.5f;
        [field: SerializeField] public EffectType LevelUpEffect { get; private set; }
        [field: SerializeField] public int ScorePerLevel { get; private set; } = 50;
    }
}