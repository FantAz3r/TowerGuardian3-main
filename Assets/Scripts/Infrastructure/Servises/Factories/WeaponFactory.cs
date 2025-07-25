using UnityEngine;

public class WeaponFactory
{
    private AttackZone _attackZone;
    private Transform _player;

    public WeaponFactory(AttackZone attackZone, Transform player)
    {
        _attackZone = attackZone;
        _player = player;
    }

    public void Create(WeaponConfig config)
    {
        Transform container = _player.GetComponentInChildren<Fist>().transform;
        PlayerAttacker attacker = container.GetComponentInParent<PlayerAttacker>();
        GameObject weaponObject = Object.Instantiate(config.Prefab.gameObject, container);

        weaponObject.transform.localPosition = Vector3.zero;
        Weapon weapon = weaponObject.GetComponent<Weapon>();
        weapon.Init(_attackZone.transform, _attackZone);
        attacker.SetWeapon(weapon);
    }
}
