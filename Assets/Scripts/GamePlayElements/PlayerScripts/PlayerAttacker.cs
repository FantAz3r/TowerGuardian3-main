using System;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private IInputService _inputService;
    private IWeapon _currentWeapon;
    private bool _canAttack = true;
    public event Action<IWeapon> WeaponSeted;

    public Weapon GetWeapon => _currentWeapon as Weapon;
    public bool CanAttack => _canAttack;

    public void Init(IInputService inputService)
    {
        _inputService = inputService;
        _inputService.AttackPerformed += TryUseWeapon;
    }

    private void Awake()
    {
        AttackZone attackZone = GetComponentInChildren<AttackZone>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _currentWeapon.Init(attackZone);
    }

    private void OnDestroy()
    {
        _inputService.AttackPerformed -= TryUseWeapon;
    }

    public void SetWeapon(IWeapon weapon)
    {
        if(_currentWeapon != null)
        {
            RemoveWeapon();
        }

        _currentWeapon = weapon;
        WeaponSeted?.Invoke(weapon);
    }

    public void RemoveWeapon()
    {
        _currentWeapon.TakeOff();
    }

    public void BanWeapon()
    {
        _currentWeapon = null;
    }

    public void TryUseWeapon()
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Attack();
        }
    }
}