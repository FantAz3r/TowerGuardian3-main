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
    private bool _active = true;

    public override AbilityType AbilityType => AbilityType.Burst;
    public float Cooldown => _config.Cooldown;

    public event Action<float, float> Cooldowning;

    private void Awake()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
        _player = GetComponentInParent<Player>();
        _attacker = _player.GetComponentInChildren<PlayerAttacker>();
        LoadAbility();
        enabled = false;
    }

    public override void Use()
    {
        if (_active)
        {
            StartCoroutine(CooldownRoutine());
            StartCoroutine(AttackRoutine());
        }
    }

    public override void Upgrade()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
    }

    public IEnumerator CooldownRoutine()
    {
        _active = false;
        float timer = 0;

        while (_config.Cooldown >= timer)
        {
            Cooldowning?.Invoke(_config.Cooldown, timer);
            timer += 1;
            yield return _oneSecond;
        }

        Cooldowning?.Invoke(_config.Cooldown, 0);
        _active = true;
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