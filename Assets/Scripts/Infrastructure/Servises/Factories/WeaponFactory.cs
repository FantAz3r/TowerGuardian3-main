using UnityEngine;

public class WeaponFactory : ICardFactory
{
    private Player _player;

    public WeaponFactory(Player player) => _player = player;

    public CardType Type => CardType.Weapon;

    public void Create(ICardConfig config)
    {
        if (config is WeaponConfig weaponConfig)
        {
            GameObject weaponObject = Object.Instantiate(weaponConfig.Prefab.gameObject, _player.Fist.transform);
            weaponObject.transform.localPosition = Vector3.zero;
            Weapon weapon = weaponObject.GetComponent<Weapon>();
            weapon.Init(_player.AttackZone);
            _player.Attacker.AddWeapon(weapon);
        }
    }
}

