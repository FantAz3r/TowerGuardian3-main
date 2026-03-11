using UnityEngine;

public class PatrolState : State
{
    private Vector3[] _patrolPoints;
    private int _currentPointIndex;
    private bool _isWaitingForNextPoint = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        Transform origin = Enemy.transform;
        float edgeSize = 10f;

        _patrolPoints = new Vector3[]
        {
            origin.position,
            origin.position + origin.right * edgeSize,
            origin.position + origin.right * edgeSize + origin.forward * edgeSize,
            origin.position + origin.forward * edgeSize
        };

        _currentPointIndex = 0;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float threshold = 1f;

        if (Enemy.Agent.GetRemainingDistance() <= threshold && _isWaitingForNextPoint == false)
        {
            _isWaitingForNextPoint = true;
            _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
        }
        else
        {
            _isWaitingForNextPoint = false;
        }

        FollowTargetPoint(_patrolPoints[_currentPointIndex]);
    }
}
