using TowerGuardian.Scripts.Enums;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        [field: SerializeField] public LevelID PreviousLevel { get; private set; }
    }
}