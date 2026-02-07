using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponConfig _config;

    private AttackZone _attackZone;
    private ISpawnerService _spawnerService;

    private float _damage;
    private float _range;
    private float _multiply;

    public event Action<int, Vector3, EntityType> HitedTarget;
    public WeaponConfig Config => _config;

    public void Init(AttackZone attackZone)
    {
        _attackZone = attackZone;
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
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
        List<Health> targets = _attackZone.GetTargets(_range);
        IEnumerable<Health> orderedByDistanceTargets = Utils.GetObjectsSortedByDistance(targets, transform.position);
        _spawnerService.SendSoundReqest(_config.HitSound, transform.position);

        foreach (var target in orderedByDistanceTargets)
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

            if (_config.IsAreaDamage == false)
            {
                return;
            }
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