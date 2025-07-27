using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
public class WeaponConfig : ScriptableObject , IConfig
{
    [SerializeField] private Weapon _prefab;
    [SerializeField] private Image _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackDelay = 1f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _multiplyToMainTarget = 2;
    [SerializeField] private TargetType _targetType;

    public Weapon Prefab => _prefab;
    public Image Icon => _icon;
    public string Name => _name;
    public string Description => _description;
    public float Damage => _damage;
    public float AttackDelay => _attackDelay;
    public float AttackRange => _attackRange;
    public float Multiply => _multiplyToMainTarget;
    public TargetType TargetType => _targetType;

    public Dictionary<string, float> GetStats()
    {
        Dictionary<string, float> stats = new Dictionary<string, float>();

        stats.Add("Damage", _damage);
        stats.Add("Attack Delay", _attackDelay);
        stats.Add("Attack Range", _attackRange);

        return stats;
    }

}