using System;
using System.Collections;
using UnityEngine;

public class PatrolState : State
{
    private EnemyStateMachine _stateMashine;
    private EnemyAnimator _animator;
    private Vector3[] _patrolPoints;
    private int _currentPointIndex;

    public PatrolState(EnemyStateMachine stateMachine, EnemyAnimator animator) : base(stateMachine )
    {
        _stateMashine = stateMachine;

    }

    public override void Enter()
    {
        Transform origin = _stateMashine.transform;
        float edgeSize = 5f;  

        _patrolPoints = new Vector3[]
        {
            origin.position,
            origin.position + origin.right * edgeSize,
            origin.position + origin.right * edgeSize + origin.forward * edgeSize,
            origin.position + origin.forward * edgeSize
        };

        _currentPointIndex = 0;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override IEnumerator UpdateRoutine()
    {
        while(true)
        {
            float threshold = 1f;
            Vector3 targetPos = _patrolPoints[_currentPointIndex];

            float distance = (_stateMashine.transform.position - targetPos).sqrMagnitude;

            if (distance < threshold * threshold)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
            else
            {
                Vector3 direction3D = targetPos - _stateMashine.transform.position;
                direction3D.y = 0f;
                Vector2 direction = new Vector2(direction3D.x, direction3D.z).normalized;

                _stateMashine.Mover.SetDirection(direction);
                _stateMashine.Rotator.SetDirection(direction);
            }

            yield return null;
        }
    }
}
