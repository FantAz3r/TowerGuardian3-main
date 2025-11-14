using System;
using System.Collections;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyStateMachine _enemy;
    private MonoBehaviour _coroutineRunner;
    private Vector3[] _patrolPoints;
    private int _currentPointIndex;
    private Coroutine _moveCoroutine;

    public event Action Attacked;

    public PatrolState(MonoBehaviour coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void Enter(EnemyStateMachine enemy)
    {
        _enemy = enemy;

        Transform origin = enemy.transform;
        float edgeSize = 5f;  

        _patrolPoints = new Vector3[]
        {
            origin.position,
            origin.position + enemy.transform.right * edgeSize,
            origin.position + enemy.transform.right * edgeSize + enemy.transform.forward * edgeSize,
            origin.position + enemy.transform.forward * edgeSize
        };

        _currentPointIndex = 0;
        _moveCoroutine = _coroutineRunner.StartCoroutine(MoveRoutine());
    }

    public void Exit()
    {
        if (_moveCoroutine != null)
            _coroutineRunner.StopCoroutine(_moveCoroutine);

        _enemy.Mover.SetDirection(Vector2.zero);
        _moveCoroutine = null;
    }

    public IEnumerator MoveRoutine()
    {
        while(true)
        {
            float threshold = 1f;
            Vector3 targetPos = _patrolPoints[_currentPointIndex];

            float distance = (_enemy.transform.position - targetPos).sqrMagnitude;

            if (distance < threshold * threshold)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
            else
            {
                Vector3 direction3D = targetPos - _enemy.transform.position;
                direction3D.y = 0f;
                Vector2 direction = new Vector2(direction3D.x, direction3D.z).normalized;

                Debug.Log(direction);
                _enemy.Mover.SetDirection(direction);
                _enemy.Rotator.SetDirection(direction);
            }

            yield return null;
        }
    }
}
