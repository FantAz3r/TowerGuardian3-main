using System.Collections.Generic;
using System.Linq;
using TowerGuardian.Enums;
using TowerGuardian.StaticData;
using UnityEngine;

namespace TowerGuardian.Factories
{
    public class WeaponFactory : ICardFactory
    {
        private Player _player;

        public WeaponFactory(Player player) => _player = player;

        public CardType Type => CardType.Weapon;

        public void Create(ICardConfig config)
        {
            List<Weapon> weapons = _player.Fist.transform.GetComponentsInChildren<Weapon>().ToList();

            foreach (var weapon in weapons)
            {
                if (config.ID == weapon.Config.ID)
                {
                    return;
                }
            }

            if (config is WeaponConfig weaponConfig)
            {
                GameObject weaponObject = Object.Instantiate(weaponConfig.Prefab.gameObject, _player.Fist.transform);
                Weapon weapon = weaponObject.GetComponent<Weapon>();
                weapon.Init(_player.AttackZone);
                _player.Attacker.AddWeapon(weapon);
            }
        }
    }
}