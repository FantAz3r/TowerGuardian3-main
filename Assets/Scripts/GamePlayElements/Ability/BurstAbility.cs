using System.Collections;
using UnityEngine;

public class BurstAbility : Ability
{
    [SerializeField] private BurstConfig _config;

    private PlayerAttacker _attacker;
    private WaitForSeconds _sleep;
    private WaitForSeconds _cooldownDelay;
    private bool _active = true;

    public override AbilityType AbilityType => AbilityType.Burst;

    private void Awake()
    {
        _sleep = new WaitForSeconds(_config.AttackDelay);
        _cooldownDelay = new WaitForSeconds(_config.Cooldown);
        _attacker = GetComponentInParent<PlayerAttacker>();
        enabled = false;
    }
   
    public override void Use()
    {
        if (_attacker.Weapon != null)
        {
            if (_active)
                StartCoroutine(Attack());
        }
        else
        {
            Debug.Log("Need Weapon");
        }
    }

    private IEnumerator Attack()
    {
        _active = false;

        for (int i = 0; i < _config.HitCount; i++)
        {
            _attacker.Weapon.ApplyDamage(); 
            yield return _sleep;
        }

        yield return _cooldownDelay;
        _active = true;
    }
}
