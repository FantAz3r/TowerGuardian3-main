using UnityEngine;

public class PickupState : State
{
    private const float AbilityCooldown = 15;
    private bool _isPickingUp; 
    private float _threshold = 1f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _isPickingUp = false;

        Enemy.StateMachine.OnStartPickup();
        Enemy.StateMachine.SetCooldown(AbilityCooldown);
        Enemy.TargetDetector.gameObject.SetActive(false);
        FindObject();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Enemy.ThrownObject == null)
        {
            OnNullThrownObject();
            return;
        }

        if (_isPickingUp == false)
        {
            FollowTargetPoint(Enemy.ThrownObject.position);

            if (Enemy.Agent.GetPathPedding() == false && Enemy.Agent.GetRemainingDistance() <= _threshold)
            {
                Enemy.AnimationAnimator.PlayPickUp();
                _isPickingUp = true;
            }
        }
        else
        {
            if (Enemy.AnimationAnimator.IsPicked)
            {
                FinishPickup();
            }
        }
    }

    private void FindObject()
    {
        Transform nearest = Enemy.ThrownObjectDetector.GetNearestResource();
        Enemy.SetThrownObject(nearest);

        if (nearest == null)
        {
            OnNullThrownObject();
        }
    }

    private void FinishPickup()
    {
        Enemy.PickUper.Pickup(Enemy.ThrownObject);
        Enemy.StateMachine.OnReadyToThrow();
    }

    private void OnNullThrownObject()
    {
        Enemy.TargetDetector.gameObject.SetActive(true);
        Enemy.StateMachine.OnNullTrownObject();
        Enemy.StateMachine.OnStopPickup();
    }
}

