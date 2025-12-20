using System.Collections;
using UnityEngine;

public abstract class State : IEnemyState
{
    private EnemyStateMachine _stateMachine;
    private bool _canExit;
    public bool CanExit => _canExit;

    public State(EnemyStateMachine stateMachine, bool canExit)
    {
        _stateMachine = stateMachine;
        _canExit = canExit;
    }
   
    public abstract void Enter();

    public abstract void Exit();

    public abstract IEnumerator UpdateRoutine();

    public void RotateTo(Vector3 target)
    {
        Vector3 direction3D = target - _stateMachine.transform.position;
        direction3D.y = 0f;
        Vector2 direction = new Vector2(direction3D.x, direction3D.z).normalized;
        _stateMachine.Rotator.SetDirection(direction);
    }

    public void SetCanExit(bool canExit)
    {
        _canExit = canExit;
    }
}
