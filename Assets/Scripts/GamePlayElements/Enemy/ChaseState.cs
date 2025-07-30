using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy _enemy;
    private Player _player;

    public ChaseState(Player player)
    {
        _player = player;
    }

    public void Enter(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Exit()
    {
        _enemy.Mover.SetDirection(Vector2.zero);
    }

    public void Update()
    {
        if (_enemy.Target == null)
            return;

        _enemy.Mover.SetDirection(_player.transform.position);
        _enemy.Rotator.SetDirection(_player.transform.position);
    }
}