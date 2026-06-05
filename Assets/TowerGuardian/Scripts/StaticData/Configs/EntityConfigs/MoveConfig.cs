using TowerGuardian.Scripts.StaticData.Configs.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.StaticData.Configs.EntityConfigs
{
    [CreateAssetMenu(fileName = "MoveConfig", menuName = "Configs/MoveConfig")]

    public class MoveConfig : ScriptableObject, IMoveConfig
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 1f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 1f;
    }
}