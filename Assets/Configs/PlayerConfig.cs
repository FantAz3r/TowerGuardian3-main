using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]

public class PlayerConfig : ScriptableObject
{
    public float MoveSpeed = 5f;
    public float RotationSpeed = 5f;
    public float MaxHealth = 1;
    public float HealthRegeneration = 1;
    public int InventoryCapacity = 1000;
}          
    
