using System;
using System.Collections;
using UnityEngine;

public class AxeThrowingAbility : Ability, ICooldownAbility
{
    [SerializeField] private AxeThrowingConfig _config;

    private PlayerAttacker _attacker;
    private Weapon _axe;
    private ThrownAxe _thrownAxe;

    private bool _active = true;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1);

    public event Action<float, float> CooldownStarted;

    public float Cooldown => _config.Cooldown;
    public override AbilityType AbilityType => AbilityType.ThrowingAxes;

    private void Awake()
    {
        _attacker = GetComponentInParent<PlayerAttacker>();
    }

    public override void Use()
    {
        if (_active)
        {
            if (_attacker.CurrentWeapon.Config.WeaponType == WeaponType.Axe)
            {
                _axe = _attacker.CurrentWeapon;
                _attacker.BanWeapon();
                StartCoroutine(CooldownRoutine());
                ThrowAxe(_axe);
            }
        }
    }

    public IEnumerator CooldownRoutine()
    {
        _active = false;
        float timer = 0f;

        while (_config.Cooldown >= timer)
        {
            CooldownStarted?.Invoke(_config.Cooldown, timer);
            timer++;
            yield return _oneSecond;
        }

        CooldownStarted?.Invoke(_config.Cooldown, 0f);
        _active = true;
    }

    private void ThrowAxe(Weapon currentWeapon)
    {
        Transform weaponTransform = currentWeapon.transform;
        Vector3 start = weaponTransform.position;
        Vector3 forward = -weaponTransform.right;
        Vector3 end = start + forward * _config.FlightDistance;

        ThrownAxe axe = currentWeapon.GetComponent<ThrownAxe>();
        _thrownAxe = axe;
        _thrownAxe.Returned += SetWeapon;
        _thrownAxe.Init(weaponTransform, start, end, _config.FlightDuration, _config.Damage);
    }

    private void SetWeapon()
    {
        _attacker.AddWeapon(_axe);
        Debug.Log("axe seted");
        _thrownAxe.Returned -= SetWeapon;
    }
}