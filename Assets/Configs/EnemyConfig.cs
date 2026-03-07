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
    [field: SerializeField] public int Level { get; private set; } = 1;

    [SerializeField] private float _damageGrowthMultiplier = 2f;

    [SerializeField] private float _healthGrowthPerLevel = 3f;

    [SerializeField] private List<StateType> allowedStates;

    public IReadOnlyList<StateType> AllowedStates => allowedStates;

    public void SetLevel(int level)
    {
        Level = Mathf.Max(level, 1);
    }

    public float GetMoveSpeed()
    {
        return MoveConfig.MoveSpeed + Level;
    }

    public int GetDamage()
    {
        return Mathf.RoundToInt(Damage * Mathf.Pow(_damageGrowthMultiplier, Level));
    }

    public float GetMaxHealth()
    {
        return Mathf.RoundToInt(HealthConfig.MaxHealth * Mathf.Pow(_healthGrowthPerLevel, Level));
    }
}

