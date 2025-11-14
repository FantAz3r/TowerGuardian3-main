using System;
using System.Collections;

public interface IEnemyState
{
    void Enter();
    void Exit();
    IEnumerator UpdateRoutine();
}