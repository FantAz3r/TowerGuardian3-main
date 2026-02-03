using System;
using System.Collections;
using UnityEngine;

public class AxeThrowingAbility : UsebleAbility, ICooldownAbility
{
    [SerializeField] private AxeThrowingConfig _config;

    private PlayerAttacker _attacker;
    private Player _player;
    private Weapon _axe;
    private ThrownAxe _thrownAxe;

    private WaitForSeconds _oneSecond = new WaitForSeconds(1);

    public event Action<float, float> Cooldowning;

    public float Cooldown => _config.Cooldown;
    public override AbilityType Type => AbilityType.ThrowingAxes;
    public override AbilityConfig Config => _config;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _attacker = _player.Attacker;
        _attacker.WeaponSeted += CheckWeapon;
    }

    private void OnEnable()
    {
        CheckWeapon(_attacker.CurrentWeapon);
    }

    private void OnDestroy()
    {
        _attacker.WeaponSeted -= CheckWeapon;
    }

    public override void Use()
    {
        if (IsLock == false)
        {
            if (_attacker.CurrentWeapon.Config.WeaponType == WeaponType.Axe)
            {
                _axe = _attacker.CurrentWeapon;
                _attacker.DeactivateWeapon();
                StartCoroutine(CooldownRoutine());
                ThrowAxe(_axe);
            }
        }
    }

    public IEnumerator CooldownRoutine()
    {
        base.LockAbility();
        float timer = 0f;

        while (_config.Cooldown >= timer)
        {
            Cooldowning?.Invoke(_config.Cooldown, timer);
            timer++;
            yield return _oneSecond;
        }

        Cooldowning?.Invoke(_config.Cooldown, 0f);
        base.UnlockAbility();
    }

    private void CheckWeapon(IWeapon weapon)
    {
        if (weapon == null)
            return;

        if (weapon.Config.WeaponType == WeaponType.Axe)
        {
            base.UnlockAbility();
        }
        else
        {
            base.LockAbility();
        }
    }

    private void ThrowAxe(Weapon currentWeapon)
    {
        Vector3 start = transform.position;
        Vector3 forward = _attacker.transform.forward;
        Vector3 end = start + forward * _config.FlightDistance;

        _thrownAxe = currentWeapon.GetComponent<ThrownAxe>();
        _thrownAxe.Returned += Return;
        _thrownAxe.Throw(start, end, _config.FlightDuration, _config.Damage);
    }

    private void Return()
    {
        _attacker.ActivateWeapon(_axe);
        _thrownAxe.Returned -= Return;
    }
}