using System.Collections;
using UnityEngine;

public class DieState : State, IEnemyState
{
    private WaitForSeconds _dieAnimation = new WaitForSeconds(3);

    public DieState(EnemyStateMachine stateMachine, bool canExit) : base(stateMachine, false)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override IEnumerator UpdateRoutine()
    {
        yield return _dieAnimation;
        SetCanExit(true);
    }
}
