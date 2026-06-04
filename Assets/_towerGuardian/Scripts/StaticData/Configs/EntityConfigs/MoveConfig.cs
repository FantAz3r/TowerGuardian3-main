using UnityEngine;

namespace TowerGuardian.StaticData
{
    [CreateAssetMenu(fileName = "MoveConfig", menuName = "Configs/MoveConfig")]

    public class MoveConfig : ScriptableObject, IMoveConfig
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 1f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 1f;
    }
}