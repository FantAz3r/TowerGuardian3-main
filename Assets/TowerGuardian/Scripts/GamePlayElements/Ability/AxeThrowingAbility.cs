using System;
using System.Collections;
using TowerGuardian.Scripts.Enums;
using TowerGuardian.Scripts.GamePlayElements.Ability.AbilityInfrastructure;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using TowerGuardian.Scripts.GamePlayElements.Weapons;
using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Factories;
using TowerGuardian.Scripts.StaticData.Configs;
using TowerGuardian.Scripts.StaticData.Configs.AbilityConfigs;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Ability
{
    public class AxeThrowingAbility : UsebleAbility, ICooldownAbility
    {
        [SerializeField]
        private AxeThrowingConfig _config;
        private Player _player;
        private Weapon _axe;
        private ThrownAxe _thrownAxe;

        public event Action<float, float> Cooldowning;

        public float Cooldown => _config.Cooldown;

        public override AbilityType Type => AbilityType.ThrowingAxes;

        public override AbilityConfig Config => _config;

        public bool IsCooldowning { get; private set; }

        private void Awake()
        {
            _player = ServiceLocator.Get<IGameFactory>().Player;
            _player.Attacker.WeaponSeted += CheckWeapon;
            CheckWeapon(_player.Attacker.CurrentWeapon);
        }

        private void OnDestroy()
        {
            _player.Attacker.WeaponSeted -= CheckWeapon;
        }

        public override void Use()
        {
            if (IsLock)
            {
                return;
            }

            if (IsCooldowning)
            {
                return;
            }

            if (_player.Attacker.CurrentWeapon.Config.WeaponType == WeaponType.Axe && _player.Attacker.CurrentWeapon.gameObject.activeSelf)
            {
                _axe = _player.Attacker.CurrentWeapon;
                _player.Attacker.DeactivateWeapon();
                StartCoroutine(CooldownRoutine());
                ThrowAxe(_axe);
            }
        }

        public IEnumerator CooldownRoutine()
        {
            IsCooldowning = true;
            float timer = 0f;

            while (_config.Cooldown >= timer)
            {
                Cooldowning?.Invoke(_config.Cooldown, timer);
                timer += Time.deltaTime;
                yield return null;
            }

            Cooldowning?.Invoke(_config.Cooldown, 0f);
            IsCooldowning = false;
        }

        private void CheckWeapon(IWeapon weapon)
        {
            if (weapon == null)
            {
                return;
            }

            if (weapon.Config.WeaponType == WeaponType.Axe)
            {
                UnlockAbility();
            }
            else
            {
                LockAbility();
            }
        }

        private void ThrowAxe(Weapon currentWeapon)
        {
            Vector3 start = transform.position;
            Vector3 forward = _player.Attacker.transform.forward;
            Vector3 end = start + (forward * _config.FlightDistance);

            _thrownAxe = currentWeapon.GetComponent<ThrownAxe>();
            _thrownAxe.Returned += Return;
            _thrownAxe.Throw(start, end, _config.FlightDuration, _axe.Config.Damage);
        }

        private void Return()
        {
            _player.Attacker.ActivateWeapon(_axe);
            _thrownAxe.Returned -= Return;
        }
    }
}