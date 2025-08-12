using System;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private IInputService _inputService;
    private IWeapon _currentWeapon;

    public event Action<IWeapon> WeaponSeted;

    public Weapon Weapon => _currentWeapon as Weapon;

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
        _currentWeapon.TakeOff();
        _currentWeapon = weapon;
        WeaponSeted?.Invoke(weapon);
    }

    public void RemoveWeapon()
    {
        _currentWeapon.TakeOff();
    }

    public void TryUseWeapon()
    {
        _currentWeapon.Attack();
    }
}