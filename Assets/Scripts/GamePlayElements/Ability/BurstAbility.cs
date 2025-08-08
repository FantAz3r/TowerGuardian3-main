using System;
using System.Collections;
using UnityEngine;

public class BurstAbility : Ability, ICooldownAbility
{
    [SerializeField] private BurstConfig _config;

    private PlayerAttacker _attacker;
    private WaitForSeconds _sleep;
    private bool _active = true;

    public override AbilityType AbilityType => AbilityType.Burst;
    public float Cooldown => _config.Cooldown;

    public event Action<float,float> CooldownStarted;  

    private void Awake()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
        _attacker = GetComponentInParent<PlayerAttacker>();
        enabled = false;
    }

    public override void Use()
    {
        if (_attacker.Weapon != null)
        {
            if (_active)
            {
                StartCoroutine(AttackRoutine());
                StartCoroutine(CooldownRoutine());
            }
        }
        else
        {
            Debug.Log("Need Weapon");
        }
    }

    private IEnumerator AttackRoutine()
    {
        _active = false;

        for (int i = 0; i < _config.HitCount; i++)
        {
            _attacker.Weapon.ApplyDamage();
            yield return _sleep;
        }
    }

    public IEnumerator CooldownRoutine()
    {
        float timer = _config.Cooldown;

        while (_config.Cooldown > 0f)
        {
            CooldownStarted?.Invoke(_config.Cooldown, timer);
            timer -= Time.deltaTime;
            yield return null;
        }

        _active = true;
    }
}