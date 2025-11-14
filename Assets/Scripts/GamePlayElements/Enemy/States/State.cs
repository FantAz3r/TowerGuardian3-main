using System.Collections;
using UnityEngine;

public abstract class State : IEnemyState
{
    private EnemyStateMachine _stateMachine;
    private Coroutine _updateCoroutine;

    public State(EnemyStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        _updateCoroutine = _stateMachine.StartCoroutine(UpdateRoutine());
    }

    public virtual void Exit()
    {
        _stateMachine.StopCoroutine(UpdateRoutine());
        _updateCoroutine = null;
        _stateMachine.Mover.SetDirection(Vector2.zero);
    }

    public virtual IEnumerator UpdateRoutine()
    {
        yield break;
    }
}
