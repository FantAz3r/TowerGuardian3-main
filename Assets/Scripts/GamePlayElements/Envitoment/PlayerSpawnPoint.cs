using UnityEngine;

public class PlayerSpawnPoint: MonoBehaviour
{
    [field: SerializeField] public LevelID PreviousLevel { get; private set; }
}