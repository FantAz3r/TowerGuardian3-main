using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EscapeState : State
{
   // private Transform _player;
   // private NavMeshAgent _agent;
   // private float _escapeDistance = 10f;
   // private Vector3 _escapeTarget;
   //
   // public EscapeState(
   //     EnemyStateMachine stateMachine,
   //     Transform player,
   //     NavMeshAgent agent
   //     ) : base(stateMachine, false)
   // {
   //     _player = player;
   //     _agent = agent;
   // }
   //
   // public override void Enter()
   // {
   //     Vector3 directionAwayFromPlayer = (_agent.transform.position - _player.position).normalized;
   //     _escapeTarget = _agent.transform.position + directionAwayFromPlayer * _escapeDistance;
   //     _agent.SetDestination(_escapeTarget);
   // }
   //
   // public override void Exit()
   // {
   // }
   //
   // public override IEnumerator UpdateRoutine()
   // {
   //     while (true)
   //     {
   //         if (_agent.pathPending == false && _agent.remainingDistance <= _agent.stoppingDistance)
   //         {
   //             SetCanExit(true);
   //             yield break;
   //         }
   //
   //         yield return null;
   //     }
   // }
}

