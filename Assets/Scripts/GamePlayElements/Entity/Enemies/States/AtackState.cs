using System.Collections;
using UnityEngine;

public class AttackState : State, IEnemyState
{
    private float _updateTime = 0.05f;
    private WaitForSeconds _delay;

    private EnemyStateMachine _stateMachine;
    private EnemyAnimator _animator;
    private Player _player;
    private IDemageable _playerHealth;

    public AttackState(EnemyStateMachine stateMachine, EnemyAnimator animator, Player target) : base(stateMachine, true)
    {
        _stateMachine = stateMachine;
        _animator = animator;
        _player = target;

        _delay = new WaitForSeconds(_updateTime);
        _playerHealth = _player.GetComponent<IDemageable>();
    }

    public override void Enter()
    {
        _stateMachine.Mover.SetDirection(Vector3.zero);
        _animator.Attacked += OnAnimAttackHit;
    }

    public override void Exit()
    {
        _animator.Attacked -= OnAnimAttackHit;
        _animator.SuspendAttack();
    }

    public override IEnumerator UpdateRoutine()
    {
        if (_player == null)
            yield break;

        float speedForAnimator = 0f;
        float attackCooldown = _stateMachine.Config.AttackCooldown;
        float timeSinceLastAttack = 0f;
        _animator.PlayAttack();

        while (_player.IsAlive)
        {
            RotateTo(_player.transform.position);
            _animator.UpdateSpeed(speedForAnimator);

            if (timeSinceLastAttack >= attackCooldown)
            {
                _animator.PlayAttack();
                timeSinceLastAttack = 0f;
            }
            else
            {
                timeSinceLastAttack += Time.deltaTime;
            }

            yield return _delay;
        }
    }

    private void OnAnimAttackHit()
    {
        int damage = _stateMachine.Config.Damage;
        _playerHealth.TakeDamage(damage);
    }
}