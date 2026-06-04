using UnityEngine;

namespace TowerGuardian.StaticData
{
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
        [field: SerializeField] public AudioClip JumpSound { get; private set; }
        [field: SerializeField] public int ThrowDamage { get; private set; }
        [field: SerializeField] public int ThronDamage { get; private set; }
        [field: SerializeField] public AudioClip ThronAttackSound { get; private set; }
        [field: SerializeField] public int LevaRockDamage { get; private set; }
        [field: SerializeField] public AudioClip UltimateSound { get; private set; }
        [field: SerializeField] public int Level { get; private set; } = 0;
        [field: SerializeField] public RuntimeAnimatorController Controller { get; private set; }

        [SerializeField] private float _damageGrowthMultiplier = 2f;

        public void SetLevel(int level)
        {
            Level = Mathf.Max(level, 0);
            HealthConfig.SetLevel(level);
        }

        public float GetMoveSpeed()
        {
            return MoveConfig.MoveSpeed + Level / 2;
        }

        public int GetDamage()
        {
            return Mathf.RoundToInt(Damage * Mathf.Pow(_damageGrowthMultiplier, Level));
        }
    }
}