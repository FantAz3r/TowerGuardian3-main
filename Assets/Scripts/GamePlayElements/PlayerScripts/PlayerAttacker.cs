using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private List<Weapon> _weaponsInInventory = new();
    private Weapon _currentWeapon;
    private Weapon _previousWeapon = null;
    private Coroutine _attackCoroutine;
    private WaitForSeconds _attackDelay; 
    private WaitForSeconds _delay;
    private float _defoultAttackTime = 1f;
    private float _emptyTargetAttackDelay = 0.1f;
    private ISpawnerService _spawnerService;

    public event Action<IWeapon> WeaponSeted;
    public event Action<IWeapon> WeaponRemoved;
    public event Action<IWeapon, float> Attacked;

    public IReadOnlyList<Weapon> WeaponsInInventory => _weaponsInInventory;
    public Weapon CurrentWeapon => _currentWeapon;
    public Weapon PreviousWeapon => _previousWeapon;

    public void Init(ISpawnerService spawnerService)
    {
        _spawnerService = spawnerService;

        SetWeapon(_currentWeapon.Config);
        StartAttacking();
    }

    private void Awake()
    {
        _attackDelay = new WaitForSeconds(_defoultAttackTime);
        _delay = new WaitForSeconds(_emptyTargetAttackDelay);

        AttackZone attackZone = GetComponentInChildren<AttackZone>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _currentWeapon.Init(attackZone);
        _weaponsInInventory.Add(_currentWeapon);
        _currentWeapon.HitedTarget += SendReqest;
    }

    public void AddWeapon(Weapon weapon)
    {
        _weaponsInInventory.Add(weapon);
        if(_currentWeapon != weapon)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public void SetWeapon(WeaponConfig config)
    {
        foreach (var weapon in _weaponsInInventory)
        {
            if (weapon.Config.Name == config.Name)
            {
                RemoveWeapon();
                _previousWeapon = _currentWeapon;
                _currentWeapon = weapon;
                UpdateWeapon(_currentWeapon);
                WeaponSeted?.Invoke(_currentWeapon);

                StartAttacking();
                _currentWeapon.HitedTarget += SendReqest;
                return;
            }
        }
    }

    public void RemoveWeapon()
    {
        StopAttacking();
        _currentWeapon.HitedTarget -= SendReqest;
        WeaponRemoved?.Invoke(_currentWeapon);
    }

    public void EquipWeapon()
    {
        _currentWeapon?.Equip();
    }

    public void TakeOff()
    {
        _previousWeapon.TakeOff();
    }

    public void BanWeapon()
    {
        StopAttacking();
        _currentWeapon = null;
    }

    public void AttackAction(float attackDelay)
    {
        Attacked?.Invoke(_currentWeapon, attackDelay);
    }

    public void OnAnimationAttack()
    {
        _currentWeapon.Attack();
    }

    private void UpdateWeapon(Weapon weapon)
    {
        _currentWeapon.SetStats(_currentWeapon.Config.Damage, _currentWeapon.Config.AttackRange);
        _attackDelay = new WaitForSeconds(weapon.Config.AttackDelay);
    }

    private void StartAttacking()
    {
        if (_currentWeapon == null)
            return;

        _attackCoroutine = StartCoroutine(AttackRoutine());
    }

    private void StopAttacking()
    {
        if (_attackCoroutine == null)
            return;

        StopCoroutine(_attackCoroutine);
        _attackCoroutine = null;
    }

    private IEnumerator AttackRoutine()
    {
        while (_currentWeapon != null)
        {
            if (_currentWeapon.HasTargets())
            {
                AttackAction(_currentWeapon.Config.AttackDelay);
                yield return _attackDelay;
            }
            else
            {
                yield return _delay;
            }
        }

        _attackCoroutine = null;
    }

    private void SendReqest(int damage, Vector3 position, EntityType type)
    {
        _spawnerService.SendReqest(SpawnerType.Resources, type, position, damage);
        _spawnerService.SendReqest(SpawnerType.Text, type, position, damage);
    }

    private void OnDestroy()
    {
        StopAttacking();
    }
}