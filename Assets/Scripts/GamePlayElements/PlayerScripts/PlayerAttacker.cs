using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    private IInputService _inputService;
    private IWeapon _currentWeapon;

    public void Init(IInputService inputService, WeaponFactory factory)
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
        _currentWeapon = weapon;
    }

    public void TryUseWeapon()
    {
        _currentWeapon.Attack();
    }
}
