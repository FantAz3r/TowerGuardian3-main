using System.ComponentModel;
using UnityEngine;

public class WeaponFactory: ICardFactory
{
    private AttackZone _attackZone;
    private Transform _player;
    private Fist _container;
    private PlayerAttacker _attacker;

    public WeaponFactory(Transform player, AttackZone attackZone)
    {
        _player = player;
        _attackZone = attackZone;

        _container = _player.GetComponentInChildren<Fist>();
        _attacker = _container.GetComponentInParent<PlayerAttacker>();
    }

    public CardType Type => CardType.WeaponSetter;

    public void ActivateCard(ICardConfig config)
    {
        if (config is WeaponConfig weaponConfig)
        {
            Create(weaponConfig);
        }
    }

    private void Create(WeaponConfig config)
    {
        foreach (var item in _attacker.WeaponsInInventory)
        {
            if (item.Config.ID == config.ID)
            {
                _attacker.AddWeapon(item);
                return;
            }
        }

        GameObject weaponObject = Object.Instantiate(config.Prefab.gameObject, _container.transform);
        weaponObject.transform.localPosition = Vector3.zero;
        Weapon weapon = weaponObject.GetComponent<Weapon>();
        weapon.Init(_attackZone);
        _attacker.AddWeapon(weapon);
    }
}
