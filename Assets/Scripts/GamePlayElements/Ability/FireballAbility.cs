using System;
using System.Collections;
using UnityEngine;

public class FireballAbility : Ability, ICooldownAbility
{
    [SerializeField] private FireballConfig _config;

    private Vector3 _offset = new Vector3(0, 1, 0);
    private Player _player;
    private Coroutine _cooldownRoutine;
    public override AbilityType Type => AbilityType.FireBall;
    public override AbilityConfig Config => _config;
    public bool IsCooldowning { get; private set; } = false;
    public float Cooldown => _config.Cooldown;


    public event Action<float, float> Cooldowning;


    private void OnEnable()
    {
        _player = GetComponentInParent<Player>();
        _cooldownRoutine = StartCoroutine(CooldownRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_cooldownRoutine);
    }

    public IEnumerator CooldownRoutine()
    {
        while (enabled)
        {
            if (IsCooldowning == false)
            {
                IsCooldowning = true;

                float timer = 0f;

                while (timer < _config.Cooldown)
                {
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
        var fireballInstance = Instantiate(fireballPrefab, spawnPosition, _player.PlayerMover.transform.rotation);
        var projectile = fireballInstance.GetComponent<Fireball>();

        if (projectile != null)
        {
            Vector3 direction = _player.PlayerMover.transform.forward.normalized;
            Vector3 targetPoint = spawnPosition + direction * _config.MaxFlyDistance;

            projectile.Init(targetPoint, _config);
        }
    }
}
