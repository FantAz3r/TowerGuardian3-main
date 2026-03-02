using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public MoveConfig MoveConfig { get; private set; }
    [field: SerializeField] public HealthConfig HealthConfig { get; private set; }
    [field: SerializeField] public float DetectionRadius { get; private set; } = 10f;
    [field: SerializeField] public int Damage { get; private set; } = 10;
    [field: SerializeField] public float AttackRange { get; private set; } = 1.5f;
    [field: SerializeField] public AudioClip HitSound { get; private set; }
    [field: SerializeField] public Vector3 AttackAriaCenter { get; private set; }
    [field: SerializeField] public float AttackCooldown { get; private set; } = 1f;
    [field: SerializeField] public float JumpDamage { get; private set; } = 15;
    [field: SerializeField] public int ThrowDamage { get; private set; }

    [SerializeField] private List<StateType> allowedStates;

    public IReadOnlyList<StateType> AllowedStates => allowedStates;
}

