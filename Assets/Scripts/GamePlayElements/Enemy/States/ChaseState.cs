using System;
using System.Collections;
using UnityEngine;

public class ChaseState : State 
{
    private float _updateTime = 0.05f;
    private EnemyStateMachine _stateMachine;
    private Transform _player;
    private WaitForSeconds _sleep;

    public ChaseState(EnemyStateMachine stateMachine, Transform player) : base(stateMachine)
    {
        _stateMachine = stateMachine;
        _player = player;
    }

    public override void Enter()
    {
        _sleep = new WaitForSeconds(_updateTime);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override IEnumerator UpdateRoutine()
    {
        while (true)
        {
            float sqrDistance = Vector3.SqrMagnitude(_stateMachine.transform.position - _player.position);
            float attackRangeSqr = _stateMachine.Config.AttackRange * _stateMachine.Config.AttackRange;

            Vector3 toPlayer = _player.position - _stateMachine.transform.position;
            toPlayer.y = 0f;
            Vector3 direction = toPlayer.normalized;
            Vector2 dirFlat = new Vector2(direction.x, direction.z);
            _stateMachine.Rotator.SetDirection(dirFlat);
            _stateMachine.Mover.SetDirection(direction);

            yield return _sleep;
        }
    }
}