public interface IEnemyState
{
    void Enter(EnemyStateMachine enemy);
    void Exit();
    void Update();
}