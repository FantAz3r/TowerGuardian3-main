using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class JumpState : State
{
   //private EnemyStateMachine _stateMachine;
   //private EnemyAnimator _animator;
   //private Transform _player;
   //private NavMeshAgent _agent;
   //
   //public JumpState(EnemyStateMachine stateMachine, EnemyAnimator animator, Transform target,  NavMeshAgent agent) : base(stateMachine, false)
   //{
   //    _stateMachine = stateMachine;
   //    _animator = animator;
   //    _player = target;
   //    _agent = agent;
   //}
   //
   //public override void Enter()
   //{
   //    _agent.isStopped = false;
   //    _animator.Attacked += OnAnimAttackJump;
   //}
   //
   //public override void Exit()
   //{
   //    _animator.Attacked -= OnAnimAttackJump;
   //    _animator.SuspendAttack();
   //    _agent.isStopped = true;
   //}
   //
   //public override IEnumerator UpdateRoutine()
   //{
   //    _agent.SetDestination(_player.position);
   //    _animator.PlayAttack();
   //    yield return null;
   //}
   //
   //private void OnAnimAttackJump()
   //{
   //    var playerHealth = _player.GetComponent<IDemageable>();
   //    int damage = (int)_stateMachine.Config.JumpDamage;
   //
   //    if (playerHealth != null)
   //    {
   //        playerHealth.TakeDamage(damage);
   //    }
   //
   //    SetCanExit(true);
   //}
}
