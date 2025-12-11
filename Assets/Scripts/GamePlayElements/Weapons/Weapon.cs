using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponConfig _config;

    private AttackZone _attackZone;
    private float _damage;
    private float _range;
    private float _multiply;

    public event Action<int, Vector3, EntityType> HitedTarget;

    public WeaponConfig Config => _config;

    public void Init(AttackZone attackZone)
    {
        _attackZone = attackZone;
    }

    private void OnEnable()
    {
        UpdateLevel();
    }

    public void SetStats(float damage, float range)
    {
        _damage = damage;
        _range = range;
    }

    public void Equip()
    {
        gameObject.SetActive(true);
    }

    public void TakeOff()
    {
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        IEnumerable<Health> targets = _attackZone.GetTargets(_range);

        foreach (var target in targets)
        {
            if (target == null)
                continue;

            float damageToDeal = _damage;

            if (target.GetHealthType() == _config.TargetType)
            {
                damageToDeal *= _multiply;
            }

            HitedTarget?.Invoke(Mathf.RoundToInt(Mathf.Min(damageToDeal, target.CurrentHealth)), target.transform.position, target.GetHealthType());
            target.TakeDamage(damageToDeal);
        }
    }

    public bool HasTargets()
    {
        IEnumerable<Health> targets = _attackZone.GetTargets(_range);

        if (targets.Count() == 0)
            return false;

        return targets.Count() > 0;
    }

    public void UpdateLevel()
    {
        _damage = _config.GetDamage(_config.Level);
        _range = _config.GetAttackRange(_config.Level);
        _multiply = _config.GetMultiply(_config.Level);
    }
}