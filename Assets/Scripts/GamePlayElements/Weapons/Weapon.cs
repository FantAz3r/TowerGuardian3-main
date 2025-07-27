using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponConfig _config;

    private AttackZone _attackZone;
    private Transform _attackPoint;
    private WaitForSeconds _sleep;
    private float _damage;
    private float _range;
    private bool _canAttack = true;

    public void Init(Transform attackPoint, AttackZone attackZone)
    {
        _attackPoint = attackPoint;
        _attackZone = attackZone;
    }

    private void Awake()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
        _damage = _config.Damage;
        _range = _config.AttackRange;
    }

    public void Attack()
    {
        if (_canAttack == false)
            return;

        _canAttack = false;
        IEnumerable<IDemageable> targets = _attackZone.GetTargets(_attackPoint, _range);

        foreach (var target in targets)
        {
            if (target == null)
                continue;

            float damageToDeal = _damage;

            if (target.GetTargetType() == _config.TargetType)
            {
                damageToDeal *= _config.Multiply;
            }

            target.TakeDamage(damageToDeal);
        }

        StartCoroutine(Delay());
    }

    public void TakeOff()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator Delay()
    {
        yield return _sleep;
        _canAttack = true;
    }
}