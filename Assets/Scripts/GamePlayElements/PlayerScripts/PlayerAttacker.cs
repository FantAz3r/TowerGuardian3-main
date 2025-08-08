using System;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private IInputService _inputService;
    private IWeapon _currentWeapon;

    public event Action<IWeapon> WeaponSeted;

    public IWeapon Weapon => _currentWeapon;

    public void Init(IInputService inputService)
    {
        _inputService = inputService;
        _inputService.AttackPerformed += TryUseWeapon;
    }

    private void Awake()
    {
        AttackZone attackZone = GetComponentInChildren<AttackZone>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _currentWeapon.Init(attackZone.transform, attackZone);
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

    public void TryUseWeapon()
    {
        _currentWeapon.Attack();
    }
}