using System;
using System.Collections;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private float _updateTime = 0.05f;
    private EnemyStateMachine _enemy;
    private Player _player;
    private Coroutine _attackCoroutine;
    private MonoBehaviour _coroutineRunner;

    private WaitForSeconds _wait;
    private WaitForSeconds _sleep;

    private IDemageable _playerHealth;
    private ISpawnerService _spawnerService;

    public event Action Attacked;

    public ChaseState(Player player, MonoBehaviour coroutineRunner, ISpawnerService spawnerService)
    {
        _spawnerService = spawnerService;
        _player = player;
        _coroutineRunner = coroutineRunner;
        _playerHealth = _player?.GetComponent<IDemageable>();
    }

    public void Enter(EnemyStateMachine enemy)
    {
        _enemy = enemy;
        _wait = new WaitForSeconds(_enemy.Config.AttackCooldown);
        _sleep = new WaitForSeconds(_updateTime);

        _attackCoroutine = _coroutineRunner.StartCoroutine(MoveRoutine());
    }

    public void Exit()
    {
        _coroutineRunner.StopCoroutine(_attackCoroutine);
        _attackCoroutine = null;
        _enemy.Mover.SetDirection(Vector3.zero);
    }

    public IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (_enemy == null || _player == null)
                yield break;

            float distance = Vector3.SqrMagnitude(_enemy.transform.position - _player.transform.position);
            float attackRange = _enemy.Config.AttackRange;

            Vector3 direction3D = (_player.transform.position - _enemy.transform.position);
            direction3D.y = 0f;
            Vector3 direction = direction3D.normalized;
            Vector2 directionFlat = new Vector2(direction.x, direction.z);
            _enemy.Rotator.SetDirection(directionFlat);

            if (distance <= attackRange * attackRange)
            {
                _enemy.Mover.SetDirection(Vector3.zero);

                AttackAction();
                yield return _sleep;
            }
            else
            {
                _enemy.Mover.SetDirection(direction);
                yield return _sleep;
            }
        }
    }

    public void AttackAction()
    {
        Attacked?.Invoke();
    }

    public void ApplyDamage()
    {
        if (_playerHealth == null)
            return;

        _playerHealth.TakeDamage(_enemy.Config.Damage);
        _spawnerService.SendReqest(SpawnerType.Text, _player.transform.position, _enemy.Config.Damage);
    }
}