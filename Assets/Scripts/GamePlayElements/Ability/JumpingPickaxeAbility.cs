using System;
using System.Collections;
using UnityEngine;

public class JumpingPickaxeAbility : UsebleAbility, ICooldownAbility
{
    [SerializeField] private JumpingPickaxeConfig _config;

    private float _cooldown = 0f;
    private bool _isActive = true;

    private Weapon _pickaxe;
    private JumpingPickaxe _jumpingPickaxe;
    private Player _player;
    private WaitForSeconds _sleep;
    private PlayerAttacker _attacker;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1);

    public event Action<float, float> Cooldowning;

    public float Cooldown => _cooldown;
    public override AbilityType AbilityType => AbilityType.BouncingPickaxe;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _attacker = _player.GetComponentInChildren<PlayerAttacker>();
    }

    public override void Use()
    {
        if (_isActive)
        {
            if (_attacker.CurrentWeapon.Config.WeaponType == WeaponType.Pickaxe)
            {
                _pickaxe = _attacker.CurrentWeapon;
                _attacker.DeactivateWeapon();
                StartCoroutine(CooldownRoutine());
                ThrowPickaxe(_pickaxe);
            }
        }
    }

    private void ThrowPickaxe(Weapon currentWeapon)
    {
        _jumpingPickaxe = currentWeapon.GetComponent<JumpingPickaxe>();
        _jumpingPickaxe.Returned += Return;
        _jumpingPickaxe.Throw(_config.BouncesCount, _config.BounceRange, _config.Damage, _config.FlySpeed);
    }

    public IEnumerator CooldownRoutine()
    {
        _isActive = false;
        float timer = 0f;

        while (_cooldown >= timer)
        {
            Cooldowning?.Invoke(_cooldown, timer);
            timer++;
            yield return _oneSecond;
        }

        Cooldowning?.Invoke(_cooldown, 0f);
        _isActive = true;
    }

    private void Return(int hitCount)
    {
        _cooldown = hitCount * _config.CooldownPerHit;
        _attacker.ActivateWeapon(_pickaxe);

        _jumpingPickaxe.Returned -= Return;

        _sleep = new WaitForSeconds(_cooldown);
        StartCoroutine(CooldownRoutine());
    }
}
