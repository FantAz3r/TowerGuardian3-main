using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State 
{
    private float _updateTime = 0.05f;
    private WaitForSeconds _sleep;

    private EnemyAnimator _animator;
    private Transform _player;
    private NavMeshAgent _agent;

    public ChaseState(EnemyStateMachine stateMachine, NavMeshAgent agent, EnemyAnimator animator, Transform target) : base(stateMachine, true)
    {
        _agent = agent ?? throw new ArgumentNullException(nameof(agent));
        _animator = animator ?? throw new ArgumentNullException(nameof(animator));
        _player = target ?? throw new ArgumentNullException(nameof(target));
    }

    public override void Enter()
    {
        _sleep = new WaitForSeconds(_updateTime);
        _agent.isStopped = false;
    }

    public override void Exit()
    {
        _agent.isStopped = true;
    }

    public override IEnumerator UpdateRoutine()
    {
        while (true)
        {
            RotateTo(_player.position);
            _agent.destination = _player.position;
            _animator.UpdateSpeed(StateMachine.Config.MoveConfig.MoveSpeed);
            yield return _sleep;
        }
    }
}