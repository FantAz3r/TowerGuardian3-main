using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class FireballAbility : Ability, ICooldownAbility
{
    [SerializeField] private FireballConfig _config;

    private Vector3 _offset = new Vector3(0, 1, 0);
    private Player _player;
    private Coroutine _cooldownRoutine;
    private Fireball _fireball;

    public event Action<float, float> Cooldowning;

    public override AbilityType Type => AbilityType.FireBall;
    public override AbilityConfig Config => _config;
    public bool IsCooldowning { get; private set; } = false;
    public float Cooldown => _config.Cooldown;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    public override void Enable()
    {
        base.Enable();
        _cooldownRoutine = StartCoroutine(CooldownRoutine());
    }

    public override void Remove()
    {
        IsCooldowning = false;

        if (_cooldownRoutine != null)
            StopCoroutine(_cooldownRoutine);
        base.Remove();
    }

    public IEnumerator CooldownRoutine()
    {
        while (IsAbilityActive)
        {
            if (IsCooldowning == false)
            {
                IsCooldowning = true;

                float timer = 0f;

                while (timer < _config.Cooldown)
                {
                    if (enabled == false)
                    {
                        yield break;
                    }

                    Cooldowning?.Invoke(_config.Cooldown, timer);
                    timer += Time.deltaTime;
                    yield return null;
                }

                Cooldowning?.Invoke(_config.Cooldown, 0f);
                IsCooldowning = false;
                ThrowFireBall();
            }

            yield return null;
        }
    }

    private void ThrowFireBall()
    {
        var fireballPrefab = _config.FireballPrefab;
        var spawnPosition = _player.PlayerMover.transform.position + _player.PlayerMover.transform.forward * 1f + _offset;

        if (_fireball == null)
        {
            _fireball = Instantiate(fireballPrefab, spawnPosition, _player.PlayerMover.transform.rotation);
        }
        else
        {
            _fireball.transform.position = _player.PlayerMover.transform.position;
            _fireball.gameObject.SetActive(true);
        }

        if (YG2.envir.isDesktop)
        {
            ShotTowards(spawnPosition);
        }
        else
        {
            Vector3 playerPosition = _player.PlayerMover.transform.position;

            if (_player == null || _player.EnemyDetector == null || _player.EnemyDetector.Targets == null)
            {
                ShotTowards(spawnPosition);
                return;
            }

            List<Health> targets = _player.EnemyDetector.Targets.Where(t => t != null).ToList();

            var priorityTargets = Utils.GetObjectsSortedByDistance(
                targets.Where(t => t.GetHealthType() == EntityType.Enemy || t.GetHealthType() == EntityType.Boss).ToList(),
                playerPosition);

            Health selectedTarget = null;

            if (priorityTargets.Count > 0)
            {
                selectedTarget = priorityTargets[0];
            }
            else
            {
                var otherTargets = Utils.GetObjectsSortedByDistance(
                    targets.Where(t => t.GetHealthType() != EntityType.Enemy && t.GetHealthType() != EntityType.Boss).ToList(),
                    playerPosition);

                if (otherTargets.Count > 0)
                {
                    selectedTarget = otherTargets[0];
                }
            }

            if (selectedTarget != null)
            {
                Vector3 directionToTarget = (selectedTarget.transform.position - spawnPosition).normalized;
                _fireball.transform.rotation = Quaternion.LookRotation(directionToTarget);
                Vector3 targetPoint = selectedTarget.transform.position;

                _fireball.Init(targetPoint, _config);
            }
            else
            {
                ShotTowards(spawnPosition);
            }
        }
    }

    private void ShotTowards(Vector3 spawnPoint)
    {
        _fireball.transform.rotation = _player.PlayerMover.transform.rotation;
        Vector3 direction = _player.PlayerMover.transform.forward.normalized;
        Vector3 targetPoint = spawnPoint + direction * _config.MaxFlyDistance;

        _fireball.Init(targetPoint, _config);
    }
}
