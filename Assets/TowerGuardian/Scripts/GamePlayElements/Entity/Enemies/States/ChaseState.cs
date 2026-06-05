using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies.States
{
    public class ChaseState : State
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            Enemy.Agent.IsStopAgent(false);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            FollowTargetPoint(Enemy.Target.position);
        }
    }
}