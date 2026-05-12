using System.Collections.Generic;
using UnityEngine;

public class FinalBossUltimate : State
{
    private const float HealAnount = 0.15f;
    private const float HealReduction = 3f;
    private const float UltimateCooldown = 75f;
    private const float SpawnInterval = 1f;
    private const float Treshold = 6f;
    private const int MaxOrbitsCount = 20;

    private bool _isUltimateStarted = false;
    private ISceneContainer _sceneContainer;
    private ISpawnerService _spawnerService;
    private Vector3 _arenaCenter;
    private List<Orbit> _orbits = new();
    private float _spawnTimer;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        _sceneContainer = ServiceLocator.Get<IGameFactory>().SceneContainer;
        base.OnStateEnter(animator, stateInfo, layerIndex);

        foreach (var item in _sceneContainer.QuestObjects)
        {
            if (item.TryGetComponent(out ArenaTrigger arena))
            {
                _arenaCenter = arena.transform.position;
            }
        }

        Enemy.StateMachine.SetUltimateCooldown(UltimateCooldown);
        Enemy.StateMachine.StatrUltimate();
        _isUltimateStarted = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float sqrDistance = (_arenaCenter - Enemy.transform.position).sqrMagnitude;


        if (sqrDistance > Treshold * Treshold)
        {
            FollowTargetPoint(_arenaCenter);
            return;
        }

        if (_isUltimateStarted == false)
        {
            StartUltimate();
            _isUltimateStarted = true;
        }

        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= SpawnInterval)
        {
            SpawnOrbit();
            Enemy.Health.Heal(Enemy.Health.MaxHealth * HealAnount * (1 / HealReduction));
            _spawnTimer = 0f;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        foreach (var item in _orbits)
        {
            item.RemoveOrbit();
            Destroy(item.gameObject);
        }

        _orbits.Clear();
    }

    private void SpawnOrbit()
    {
        while(_orbits.Count < MaxOrbitsCount)
        {
            Orbit orbit = Instantiate(Enemy.Orbit, Enemy.transform);
            orbit.Init(Random.Range(2, 4), Enemy.Config.LevaRockDamage);

            if (_orbits.Count > 1)
            {
                foreach (var item in _orbits)
                {
                    item.IncreaseOrbitRange();
                }
            }

            _orbits.Add(orbit);
        }
    }

    private void StartUltimate()
    {
        _spawnerService.SendSoundReqest(Enemy.Config.UltimateSound, Enemy.transform.position);
        Enemy.AnimationAnimator.PlaytUtlimate();
        Enemy.Agent.IsStopAgent(true);
        Enemy.Health.ImmunityEnable();

        Enemy.ForceField.Health.enabled = true;
        Enemy.ForceField.Health.Died += OnEndUltimate;
        _spawnTimer = 0;
    }

    private void OnEndUltimate()
    {
        Enemy.ForceField.Health.Died -= OnEndUltimate;
        Enemy.Health.ImmunityDisable();
        Enemy.AnimationAnimator.PlayEndUltimate();
        Enemy.StateMachine.EndUltimate();
        Enemy.ForceField.Health.enabled = false;
    }
}
