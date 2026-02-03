using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    private float _updateTime = 0.05f;
    private WaitForSeconds _delay;
    private EnemyStateMachine _stateMashine;
    private EnemyAnimator _animator;
    private Vector3[] _patrolPoints;
    private int _currentPointIndex;
    private NavMeshAgent _agent;

    public PatrolState(EnemyStateMachine stateMachine, NavMeshAgent agent, EnemyAnimator animator) : base(stateMachine, true)
    {
        _stateMashine = stateMachine;
        _agent = agent;
        _animator = animator;

        _delay = new WaitForSeconds(_updateTime);
    }

    public override void Enter()
    {
        _agent.isStopped = false;
        Transform origin = _stateMashine.transform;
        float edgeSize = 15f;

        _patrolPoints = new Vector3[]
        {
            origin.position,
            origin.position + origin.right * edgeSize,
            origin.position + origin.right * edgeSize + origin.forward * edgeSize,
            origin.position + origin.forward * edgeSize
        };

        _currentPointIndex = 0;
    }

    public override void Exit()
    {
        _agent.isStopped = true;
    }

    public override IEnumerator UpdateRoutine()
    {
        while (true)
        {
            float threshold = 0.05f;
            Vector3 targetPosition = _patrolPoints[_currentPointIndex];
            _agent.destination = targetPosition;
            RotateTo(targetPosition);


            if (_agent.remainingDistance < threshold)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
            else
            {
                _animator.UpdateSpeed(_stateMashine.Config.MoveConfig.MoveSpeed);
            }

            yield return _delay;
        }
    }
}
