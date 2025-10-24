using UnityEngine;

public class WeaponFactory: ICardFactory
{
    private AttackZone _attackZone;
    private Transform _player;

    public WeaponFactory(Transform player, AttackZone attackZone)
    {
        _player = player;
        _attackZone = attackZone;
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
        Transform container = _player.GetComponentInChildren<Fist>().transform;
        PlayerAttacker attacker = container.GetComponentInParent<PlayerAttacker>();

        foreach (var item in attacker.WeaponsInInventory)
        {
            if (item.Config == config)
            {
                attacker.AddWeapon(item);
                return;
            }
        }

        GameObject weaponObject = Object.Instantiate(config.Prefab.gameObject, container);
        weaponObject.transform.localPosition = Vector3.zero;
        Weapon weapon = weaponObject.GetComponent<Weapon>();
        weapon.Init(_attackZone);
        attacker.AddWeapon(weapon);
    }
}
