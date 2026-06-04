using UnityEngine;

public class DieState : State
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Enemy.AnimationAnimator.PlayDie();
        Enemy.StateMachine.OnDie();
    }
}
