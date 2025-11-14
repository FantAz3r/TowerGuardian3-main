using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
public class EnemyConfig : MoveConfig
{
    [Header("Detection")]
    [SerializeField] private float _detectionRadius = 10f;

    [Header("Attack")]
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private Vector3 _attackAriaCenter;
    [SerializeField] private float _attackCooldown = 1f;

    public float DetectionRadius => _detectionRadius;
    public int Damage => _damage;
    public float AttackRange => _attackRange;
    public Vector3 AttackAriaCenter => _attackAriaCenter;
    public float AttackCooldown => _attackCooldown;
}
