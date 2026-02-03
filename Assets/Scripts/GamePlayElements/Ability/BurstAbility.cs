using System;
using System.Collections;
using UnityEngine;

public class BurstAbility : UsebleAbility, ICooldownAbility
{
    [SerializeField] private BurstConfig _config;

    private Player _player;
    private PlayerAttacker _attacker;
    private WaitForSeconds _sleep;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1);

    public override AbilityConfig Config => _config;
    public override AbilityType Type => AbilityType.Burst;
    public float Cooldown => _config.Cooldown;

    public event Action<float, float> Cooldowning;

    private void Awake()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
        _player = GetComponentInParent<Player>();
        _attacker = _player.GetComponentInChildren<PlayerAttacker>();

        _attacker.WeaponDeactivated += LockAbility;
        _attacker.WeaponActivated += UnlockAbility;
        _config.Upgraded += Upgrade;

        LoadAbility();
        enabled = false;
    }

    private void OnDestroy()
    {
        _attacker.WeaponDeactivated -= LockAbility;
        _attacker.WeaponActivated -= UnlockAbility;
        _config.Upgraded -= Upgrade;
    }

    public override void Use()
    {
        if (IsLock == false)
        {
            StartCoroutine(CooldownRoutine());
            StartCoroutine(AttackRoutine());
        }
    }

    public void Upgrade()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
    }

    public IEnumerator CooldownRoutine()
    {
        base.LockAbility();
        float timer = 0;

        while (_config.Cooldown >= timer)
        {
            Cooldowning?.Invoke(_config.Cooldown, timer);
            timer += 1;
            yield return _oneSecond;
        }

        Cooldowning?.Invoke(_config.Cooldown, 0);
        base.UnlockAbility();
    }

    public override void LockAbility()
    {
        base.LockAbility();
    }

    public override void UnlockAbility()
    {
        base.UnlockAbility();
    }

    private IEnumerator AttackRoutine()
    {
        for (int i = 0; i < _config.HitCount; i++)
        {
            _attacker.AttackAction(_config.AttackDelay);
            yield return _sleep;
        }
    }

    private void LoadAbility()
    {
        if (_config.HasPlayer == false)
            return;

        Upgrade();
    }
}