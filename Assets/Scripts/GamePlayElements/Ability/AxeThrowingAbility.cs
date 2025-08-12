using System;
using System.Collections;
using UnityEngine;

public class AxeThrowingAbility : Ability, ICooldownAbility
{
    [SerializeField] private AxeThrowingConfig _config;

    private PlayerAttacker _attacker;
    private Weapon _axe;
    private bool _active = true;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1);

    public event Action<float, float> CooldownStarted;

    public float Cooldown => _config.Cooldown;
    public override AbilityType AbilityType => AbilityType.ThrowingAxes;

    private void Awake()
    {
        _attacker = GetComponentInParent<PlayerAttacker>();
        enabled = false;
    }

    public override void Use()
    {
        if (_active)
        {
            if (_attacker.Weapon.WeaponType == WeaponType.Axe)
            {
                _axe = _attacker.Weapon;
                _attacker.RemoveWeapon();
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
            timer += 1f;
            yield return _oneSecond;
        }

        CooldownStarted?.Invoke(_config.Cooldown, 0f);
        _active = true;
    }

    private void ThrowAxe(Weapon currentWeapon)
    {
        _attacker.SetWeapon(null);

        Transform weaponTransform = currentWeapon.transform;
        Vector3 start = weaponTransform.position;
        Vector3 forward = weaponTransform.forward;
        Vector3 end = start + forward * _config.FlightDistance;

        GameObject prefab = currentWeapon.Config.Prefab.gameObject;
        GameObject thrownGO = Instantiate(prefab, start, Quaternion.identity);
        ThrownAxe thrown = thrownGO.GetComponent<ThrownAxe>();

        thrown.Init(_attacker.transform, start, end, _config.FlightDuration, _config.EllipseHeight, _config.Damage);
    }
}
