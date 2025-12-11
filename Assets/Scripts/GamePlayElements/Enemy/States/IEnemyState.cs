using System;
using System.Collections;

public interface IEnemyState
{
    bool CanExit { get; }
    void Enter();
    void Exit();
    IEnumerator UpdateRoutine();
}