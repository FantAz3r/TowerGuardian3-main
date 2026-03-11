using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnterPortalState : State
{
   //private float _updateTime = 0.05f;
   //private WaitForSeconds _delay;
   //private Vector3 _targetposition;
   //private NavMeshAgent _agent;
   //
   //public EnterPortalState(EnemyStateMachine stateMachine, Vector3 portalPosition, NavMeshAgent navMeshAgent) : base(stateMachine, false)
   //{
   //    _targetposition = portalPosition;
   //    _agent = navMeshAgent;
   //    _delay = new WaitForSeconds(_updateTime);
   //}
   //
   //public override void Enter()
   //{
   //    _agent.destination = _targetposition;
   //}
   //
   //public override void Exit()
   //{
   //    _agent.isStopped = true;
   //}
   //
   //public override IEnumerator UpdateRoutine()
   //{
   //    while(_agent.isStopped == false)
   //    {
   //        RotateTo(_targetposition);
   //        yield return _delay;
   //    }
   //}
}
