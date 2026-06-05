using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies.States
{
    public abstract class State : StateMachineBehaviour
    {
        protected Enemy Enemy { get; private set; }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Enemy = animator.GetComponent<Enemy>();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public void RotateTo(Vector3 target)
        {
            Vector3 direction3D = target - Enemy.transform.position;
            direction3D.y = 0f;
            Vector2 direction = new Vector2(direction3D.x, direction3D.z).normalized;
            Enemy.Rotator.SetDirection(direction);
        }

        public void FollowTargetPoint(Vector3 point)
        {
            RotateTo(point);
            Enemy.Agent.SetDestination(point);
            Enemy.AnimationAnimator.UpdateSpeed(Enemy.Config.MoveConfig.MoveSpeed);
        }
    }
}
