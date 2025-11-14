using System;
using System.Collections;

public interface IEnemyState
{
    event Action Attacked;

    void Enter(EnemyStateMachine enemy);
    void Exit();
    IEnumerator MoveRoutine();
}