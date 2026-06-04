using System.Collections;
using System.Collections.Generic;
using TowerGuardian.Enums;
using TowerGuardian.Infrastructure;
using UnityEngine;

public class ThornAttackState : State
{
    private const float ThornAttackCooldown = 9;
    private const float AppearanceDelay = 0.1f;
    private int numberOfThorns = 20;
    private float angleOffset = 10f;
    private float spacing = 1.5f;

    private WaitForSeconds _delay = new WaitForSeconds(AppearanceDelay);
    private int _thornsSpawned = 0;
    private List<Thorn> _spawnedThorns = new List<Thorn>();

    private ICoroutineRunner _coroutineRunner;
    private ISpawnerService _spawnerService;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        Enemy.StateMachine.StartThornsAttack();
        Enemy.Agent.IsStopAgent(true);
        _thornsSpawned = 0;
        _spawnedThorns.Clear();
        Enemy.AnimationAnimator.ThornAttacked += OnStartSpawn;
        Enemy.AnimationAnimator.PlayThornsAttack();
        Enemy.StateMachine.SetCooldown(ThornAttackCooldown);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RotateTo(Enemy.Target.position);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enemy.Agent.IsStopAgent(false);
    }

    private void OnStartSpawn()
    {
        _spawnerService.SendSoundReqest(Enemy.Config.ThronAttackSound, Enemy.transform.position);
        _coroutineRunner.StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (_thornsSpawned < numberOfThorns)
        {
            SpawnThorn(CalculateThornPosition());
            _thornsSpawned++;
            yield return _delay;
        }

        Enemy.StateMachine.OnEndThornAttack();
    }

    private Vector3 CalculateThornPosition()
    {
        bool spawnFromLeftRay = (_thornsSpawned % 2 == 0);
        Vector3 toTarget = (Enemy.Target.position - Enemy.transform.position).normalized;

        float baseAngle = Mathf.Atan2(toTarget.x, toTarget.z) * Mathf.Rad2Deg;
        float baseAngleLeft = baseAngle + angleOffset;
        float baseAngleRight = baseAngle - angleOffset;

        float currentAngle;
        int indexInRay;

        if (spawnFromLeftRay)
        {
            indexInRay = (_thornsSpawned / 2);
            currentAngle = baseAngleLeft + indexInRay * spacing;
        }
        else
        {
            indexInRay = (_thornsSpawned / 2);
            currentAngle = baseAngleRight - indexInRay * spacing;
        }

        Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward;

        indexInRay = (_thornsSpawned / 2);
        Vector3 spawnPosition = Enemy.transform.position + direction * (spacing * indexInRay);

        return spawnPosition;
    }

    private void SpawnThorn(Vector3 spawnPosition)
    {
        Thorn thorn = _spawnerService.SendProjectileRequest(ProjectileType.Thorns, spawnPosition) as Thorn;
        _spawnedThorns.Add(thorn);
        thorn.Init(Enemy.Config.ThronDamage);
        thorn.Appear();
    }
}
