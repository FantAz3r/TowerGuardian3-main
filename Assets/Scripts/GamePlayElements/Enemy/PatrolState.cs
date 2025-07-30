using UnityEngine;

public class PatrolState : IEnemyState
{
    private Enemy _enemy;
    private Vector3[] _patrolPoints;
    private int _currentPointIndex = 0;

    public void Enter(Enemy enemy)
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
        float treshold = 0.2f;
        Vector3 targetPos = _patrolPoints[_currentPointIndex];
        float distance = Vector3.SqrMagnitude(_enemy.transform.position - targetPos);

        if (distance < treshold)
        {
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        }
        else
        {
            _enemy.Mover.SetDirection(targetPos);
            _enemy.Rotator.SetDirection(targetPos);
        }
    }
}