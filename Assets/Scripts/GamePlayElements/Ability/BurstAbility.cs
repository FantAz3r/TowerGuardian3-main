using System;
using System.Collections;
using UnityEngine;

public class BurstAbility : Ability, ICooldownAbility
{
    [SerializeField] private BurstConfig _config;

    private PlayerAttacker _attacker;
    private WaitForSeconds _sleep;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1);
    private bool _active = true;

    public override AbilityType AbilityType => AbilityType.Burst;
    public float Cooldown => _config.Cooldown;

    public event Action<float, float> CooldownStarted;

    private void Awake()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
        _attacker = GetComponentInParent<PlayerAttacker>();
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

    private IEnumerator AttackRoutine()
    {
        for (int i = 0; i < _config.HitCount; i++)
        {
            _attacker.AttackAction(_config.AttackDelay);
            yield return _sleep;
        }
    }

    public IEnumerator CooldownRoutine()
    {
        _active = false;
        float timer = 0;

        while (_config.Cooldown >= timer)
        {
            CooldownStarted?.Invoke(_config.Cooldown, timer);
            timer += 1;
            yield return _oneSecond;
        }

        CooldownStarted?.Invoke(_config.Cooldown, 0);
        _active = true;
    }
}