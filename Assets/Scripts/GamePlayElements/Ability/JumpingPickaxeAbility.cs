using System;
using System.Collections;
using UnityEngine;

public class JumpingPickaxeAbility : UsebleAbility, ICooldownAbility
{
    [SerializeField] private JumpingPickaxeConfig _config;

    private float _cooldown = 0f;
    private Weapon _pickaxe;
    private JumpingPickaxe _jumpingPickaxe;
    private Player _player;
    private PlayerAttacker _attacker;

    public bool IsCooldowning { get; private set; } = false;
    public float Cooldown => _cooldown;
    public override AbilityType Type => AbilityType.BouncingPickaxe;
    public override AbilityConfig Config => _config;

    public event Action<float, float> Cooldowning;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _attacker = _player.Attacker;
        _attacker.WeaponSeted += CheckWeapon;
        CheckWeapon(_player.Attacker.CurrentWeapon);
    }

    private void OnDestroy()
    {
        _attacker.WeaponSeted -= CheckWeapon;
    }

    public override void Use()
    {
        if (IsLock) return;

        if (IsCooldowning) return;

        if (_attacker.CurrentWeapon.Config.WeaponType == WeaponType.Pickaxe)
        {
            _pickaxe = _attacker.CurrentWeapon;
            _attacker.DeactivateWeapon();
            StartCoroutine(CooldownRoutine());
            ThrowPickaxe(_pickaxe);
        }
    }

    private void ThrowPickaxe(Weapon currentWeapon)
    {
        _jumpingPickaxe = currentWeapon.GetComponent<JumpingPickaxe>();
        _jumpingPickaxe.Returned += Return;
        _jumpingPickaxe.Throw(_config.BouncesCount, _config.BounceRange, _pickaxe.Config.Damage, _config.FlySpeed);
    }

    public IEnumerator CooldownRoutine()
    {
        IsCooldowning = true;
        float timer = 0f;

        while (_cooldown >= timer)
        {
            Cooldowning?.Invoke(_cooldown, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        Cooldowning?.Invoke(_cooldown, 0f);
        IsCooldowning = false;
    }

    private void CheckWeapon(IWeapon weapon)
    {
        if (weapon.Config.WeaponType == WeaponType.Pickaxe)
        {
            base.UnlockAbility();
        }
        else
        {
            base.LockAbility();
        }
    }

    private void Return(int hitCount)
    {
        _cooldown = hitCount * _config.CooldownPerHit;
        _attacker.ActivateWeapon(_pickaxe);

        _jumpingPickaxe.Returned -= Return;
        StartCoroutine(CooldownRoutine());
    }
}
