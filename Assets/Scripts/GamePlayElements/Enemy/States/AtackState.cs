using System.Collections;
using UnityEngine;

public class AttackState : State
{
    private EnemyStateMachine _stateMachine;
    private EnemyAnimator _animator;
    private Transform _player;
    private ISpawnerService _spawnerService;
    private WaitForSeconds _wait;
    
    public AttackState(EnemyStateMachine stateMachine, EnemyAnimator animator, ISpawnerService spawnerService, Transform player) : base(stateMachine)
    {
        _spawnerService = spawnerService;
        _stateMachine = stateMachine;
        _animator = animator;
        _player = player;

        _wait = new WaitForSeconds(_stateMachine.Config.AttackCooldown);
    }

    public override void Enter()
    {
        _stateMachine.Mover.SetDirection(Vector3.zero);
        _animator.Attacked += OnAnimAttackHit;
        base.Enter();
    }

    public override void Exit()
    {
        _animator.Attacked -= OnAnimAttackHit;
        base.Exit();
    }

    public override IEnumerator UpdateRoutine()
    {
        while (true)
        {
            if (_player == null)
                yield break;

            Vector3 toPlayer = _player.position - _stateMachine.transform.position;
            toPlayer.y = 0f;
            Vector2 dirFlat = new Vector2(toPlayer.normalized.x, toPlayer.normalized.z);

            _stateMachine.Rotator.SetDirection(dirFlat);
            _animator.PlayAttack();

            yield return _wait;
        }
    }

    private void OnAnimAttackHit()
    {
        var playerHealth = _player.GetComponent<IDemageable>();
        int damage = _stateMachine.Config.Damage;

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            _spawnerService.SendReqest(SpawnerType.Text, _player.transform.position, damage);
        }
    }
}