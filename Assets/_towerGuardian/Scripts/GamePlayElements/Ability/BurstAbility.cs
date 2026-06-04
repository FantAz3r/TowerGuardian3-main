using System;
using System.Collections;
using TowerGuardian.Enums;
using TowerGuardian.StaticData;
using UnityEngine;

public class BurstAbility : UsebleAbility, ICooldownAbility
{
    [SerializeField] private BurstConfig _config;

    private Player _player;
    private PlayerAttacker _attacker;
    private WaitForSeconds _sleep;

    public event Action<float, float> Cooldowning;

    public bool IsCooldowning { get; private set; } = false;
    public override AbilityConfig Config => _config;
    public override AbilityType Type => AbilityType.Burst;
    public float Cooldown => _config.Cooldown;

    private void Awake()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
        _player = GetComponentInParent<Player>();
        _attacker = _player.GetComponentInChildren<PlayerAttacker>();

        _attacker.WeaponDeactivated += LockAbility;
        _attacker.WeaponActivated += UnlockAbility;
        _config.Upgraded += Upgrade;
    }

    private void OnDestroy()
    {
        _attacker.WeaponDeactivated -= LockAbility;
        _attacker.WeaponActivated -= UnlockAbility;
        _config.Upgraded -= Upgrade;
    }

    public override void Use()
    {
        if (IsCooldowning)
            return;

        if (IsLock)
            return;

        StartCoroutine(CooldownRoutine());
        StartCoroutine(AttackRoutine());
    }

    public void Upgrade(ICardConfig useles)
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
    }

    public IEnumerator CooldownRoutine()
    {
        IsCooldowning = true;
        float timer = 0;

        while (_config.Cooldown >= timer)
        {
            Cooldowning?.Invoke(_config.Cooldown, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        Cooldowning?.Invoke(_config.Cooldown, 0);
        IsCooldowning = false;
    }

    private IEnumerator AttackRoutine()
    {
        for (int i = 0; i <= _config.HitCount; i++)
        {
            _attacker.AttackAction(_config.AttackDelay);
            yield return _sleep;
        }
    }
}