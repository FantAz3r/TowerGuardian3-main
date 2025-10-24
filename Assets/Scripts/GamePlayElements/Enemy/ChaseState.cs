using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private float _updateTime = 0.1f; 
    private EnemyStateMachine _enemy;
    private Player _player;
    private Coroutine _attackCoroutine;
    private MonoBehaviour _coroutineRunner;
    private WaitForSeconds _wait;
    private WaitForSeconds _sleep;
    private IDemageable _playerHealth;


    public event Action Attacked;
    public ChaseState(Player player, MonoBehaviour coroutineRunner)
    {
        _player = player;
        _coroutineRunner = coroutineRunner;
        _playerHealth = _player.GetComponent<IDemageable>();
    }

    public void Enter(EnemyStateMachine enemy)
    {
        _enemy = enemy;
        _attackCoroutine = _coroutineRunner.StartCoroutine(AttackRoutine());
        _wait = new WaitForSeconds(_enemy.Config.AttackCooldown);
        _sleep = new WaitForSeconds(_updateTime);
    }

    public void Exit()
    {
        _enemy.Mover.SetDirection(Vector2.zero);

        if (_attackCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    public void Update()
    {
        if (_enemy.Target == null)
            return;

        Vector3 direction = (_player.transform.position - _enemy.transform.position).normalized;
        Vector2 directionFlat = new Vector2(direction.x, direction.z);

        _enemy.Mover.SetDirection(directionFlat);
        _enemy.Rotator.SetDirection(directionFlat);
    }

    public void AttackAction()
    {
        Attacked?.Invoke();
    }

    public void ApplyDamage()
    {
        IEnumerable<IDemageable> targets = _enemy.AttackZone.GetTargets(_enemy.Config.AttackRange);

        foreach (IDemageable target in targets)
        {
            if (target == _playerHealth)
            {
                target.TakeDamage(_enemy.Config.Damage);
                break;
            }
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (_enemy.Target != null)
        {
            IEnumerable<IDemageable> targets = _enemy.AttackZone.GetTargets(_enemy.Config.AttackRange);

            if(targets.Contains(_playerHealth))
            {
                AttackAction();
                yield return _wait;
            }
            else
            {
                yield return _sleep;
            }
        }
    }
}