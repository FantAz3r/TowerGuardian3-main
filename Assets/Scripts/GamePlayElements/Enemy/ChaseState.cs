using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy _enemy;
    private Player _player;
    private Coroutine _attackCoroutine;
    private MonoBehaviour _coroutineRunner;
    private WaitForSeconds _wait;

    public ChaseState(Player player, MonoBehaviour coroutineRunner)
    {
        _player = player;
        _coroutineRunner = coroutineRunner;
        _wait = new WaitForSeconds(_enemy.Config.AttackCooldown);
    }

    public void Enter(Enemy enemy)
    {
        _enemy = enemy;
        _attackCoroutine = _coroutineRunner.StartCoroutine(AttackRoutine());
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

    private IEnumerator AttackRoutine()
    {
        IDemageable playerHealth = _player.GetComponent<IDemageable>();

        while (true)
        {
            if (_enemy.Target != null)
            {
                IEnumerable<IDemageable> targets = _enemy.AttackZone.GetTargets(_enemy.Config.AttackAriaCenter, _enemy.Config.AttackRange);

                foreach (IDemageable target in targets)
                {
                    if (target == playerHealth)
                    {
                        target.TakeDamage(_enemy.Config.Damage);
                        break;
                    }
                }
            }

            yield return _wait;
        }
    }
}