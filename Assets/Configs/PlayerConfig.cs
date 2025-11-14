using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
public class PlayerConfig : MoveConfig, ILevelConfig
{
    [SerializeField] private float _healthRegeneration = 1f;
    [SerializeField] private int _inventoryCapacity = 1000;

    [SerializeField] private float _baseLvlCost = 100f;
    [SerializeField] private float _levelCostMultiplier = 1.5f;

    public float HealthRegeneration => _healthRegeneration;
    public int InventoryCapacity => _inventoryCapacity;

    public float BaseLvlCost => _baseLvlCost;
    public float LevelCostMultiplier => _levelCostMultiplier;
}
