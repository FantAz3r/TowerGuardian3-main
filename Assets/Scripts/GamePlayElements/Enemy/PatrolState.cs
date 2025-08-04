using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyStateMachine _enemy;
    private Vector3[] _patrolPoints;
    private int _currentPointIndex;

    public void Enter(EnemyStateMachine enemy)
    {
        _enemy = enemy;

        _patrolPoints = new Vector3[]
        {
            enemy.transform.position,
            enemy.transform.position + enemy.transform.right * 5f,
            enemy.transform.position + enemy.transform.right * -5f
        };

        _currentPointIndex = 0;
    }

    public void Exit()
    {
        _enemy.Mover.Move(Vector2.zero);
    }

    public void Update()
    {
        float threshold = 0.2f;
        Vector3 targetPos = _patrolPoints[_currentPointIndex];

        float distance = Vector3.SqrMagnitude(_enemy.transform.position - targetPos);

        if (distance < threshold * threshold) 
        {
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        }
        else
        {
            Vector3 direction3D = targetPos - _enemy.transform.position;
            direction3D.y = 0f; 
            Vector2 direction = new Vector2(direction3D.x, direction3D.z).normalized;

            _enemy.Mover.SetDirection(direction);
            _enemy.Rotator.SetDirection(direction);
        }
    }

}