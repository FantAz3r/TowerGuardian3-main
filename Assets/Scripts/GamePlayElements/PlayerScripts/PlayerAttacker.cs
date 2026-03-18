using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PlayerAttacker : MonoBehaviour
{
    private List<Weapon> _weaponsInInventory = new();
    private Weapon _currentWeapon, _previousWeapon = null;
    private Coroutine _attackCoroutine;
    private WaitForSeconds _attackDelay, _delay;
    private float _defoultAttackTime = 1f;
    private float _emptyTargetAttackDelay = 0.1f;

    public event Action<IWeapon, float> Attacked;
    public event Action<IWeapon> WeaponSeted, WeaponRemoved;
    public event Action<ICardConfig> SavedWeaponAdded;
    public event Action WeaponDeactivated, WeaponActivated;
    public event Action Hited, Suspended;

    public IReadOnlyList<Weapon> WeaponsInInventory => _weaponsInInventory;
    public Weapon CurrentWeapon => _currentWeapon;
    public Weapon PreviousWeapon => _previousWeapon;

    private void Awake()
    {
        _attackDelay = new WaitForSeconds(_defoultAttackTime);
        _delay = new WaitForSeconds(_emptyTargetAttackDelay);

        AttackZone attackZone = GetComponentInChildren<AttackZone>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _currentWeapon.Init(attackZone);
        _weaponsInInventory.Add(_currentWeapon);
    }

    public void AddWeapon(Weapon weapon)
    {
        _weaponsInInventory.Add(weapon);

        if (_currentWeapon != weapon)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public void SetWeapon(WeaponConfig config)
    {
        foreach (var weapon in _weaponsInInventory)
        {
            if (weapon.Config.ID == config.ID)
            {
                RemoveWeapon();
                _previousWeapon = _currentWeapon;
                _currentWeapon = weapon;
                UpdateWeapon();
                WeaponSeted?.Invoke(_currentWeapon);

                SaveCurrentWeapon(_currentWeapon.Config);
                StartAttacking();
                return;
            }
        }
    }

    public void RemoveWeapon()
    {
        if (_currentWeapon != null)
        {
            StopAttacking();
            WeaponRemoved?.Invoke(_currentWeapon);
        }
    }

    public void DeactivateWeapon()
    {
        StopAttacking();
        WeaponDeactivated?.Invoke();
        _currentWeapon = null;
    }

    public void ActivateWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        UpdateWeapon();
        WeaponActivated?.Invoke();
        StartAttacking();
    }

    public void AttackAction(float attackDelay)
    {
        Attacked?.Invoke(_currentWeapon, attackDelay);
    }

    public void OnEquipWeapon()
    {
        _currentWeapon?.Equip();
    }

    public void OnTakeOffWeapon()
    {
        foreach(var weapon in _weaponsInInventory)
        {
            weapon.gameObject.SetActive(false);
        }

        _previousWeapon?.TakeOff();
    }

    public void OnAnimationAttack()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Attack();
            Hited?.Invoke();
        }
    }

    public void LoadCurrentWeapon()
    {
        if (YG2.saves.CurrentWeapon.ID == default)
        {
            SetWeapon(_currentWeapon.Config);
        }
        else
        {
            foreach (var weapon in _weaponsInInventory)
            {

                if (weapon.Config.ID == YG2.saves.CurrentWeapon.ID)
                {
                    SetWeapon(weapon.Config);
                    SavedWeaponAdded?.Invoke(weapon.Config);
                }
            }
        }
    }

    private void UpdateWeapon()
    {
        _currentWeapon.SetStats(_currentWeapon.Config.Damage, _currentWeapon.Config.AttackRange);
        _attackDelay = new WaitForSeconds(_currentWeapon.Config.AttackDelay);
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
        yield return _delay;

        while (_currentWeapon != null)
        {
            if (_currentWeapon.HasTargets())
            {
                UpdateWeapon();
                AttackAction(_currentWeapon.Config.AttackDelay);
                yield return _attackDelay;
            }
            else
            {
                Suspended?.Invoke();
                yield return _delay;
            }
        }

        _attackCoroutine = null;
    }

    private void OnDestroy()
    {
        StopAttacking();
    }

    private void SaveCurrentWeapon(WeaponConfig config)
    {
        YG2.saves.CurrentWeapon = config.CreateSaveData(true);
    }
}