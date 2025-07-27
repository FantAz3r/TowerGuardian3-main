using UnityEngine;

public class WeaponFactory: ICardFactory
{
    private AttackZone _attackZone;
    private Transform _player;

    public WeaponFactory(AttackZone attackZone, Transform player)
    {
        _attackZone = attackZone;
        _player = player;
    }

    public CardType Type => CardType.WeaponSetter;

    public void ActivateCard(ICardConfig config)
    {
       
        if (config is WeaponConfig weaponConfig)
        {
            Create(weaponConfig);
        }
        else
        {
            Debug.LogWarning("Ожидался WeaponCard");
        }
    }

    private void Create(WeaponConfig config)
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
