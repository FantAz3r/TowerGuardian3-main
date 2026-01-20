using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField] public Platform ShopPlatform { get; private set; } 
    [field: SerializeField] public TowerDoor Door { get; private set; } 
    [field: SerializeField] public StairsTrigger StairsFirstFloor { get; private set; } 
}
