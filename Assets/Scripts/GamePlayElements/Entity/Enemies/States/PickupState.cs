using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PickupState : State
{
    private float _updateTime = 0.05f;
    private WaitForSeconds _delay;

    private EnemyAnimator _animator;
    private ThrownObjectDetector _objectDetector;
    private PickUper _pickUper;
    private NavMeshAgent _agent;
    private Transform _thrownObject;
    private bool _hasObject = false;

    public PickupState(
        EnemyStateMachine stateMachine,
        EnemyAnimator animator,
        ThrownObjectDetector objectDetector,
        NavMeshAgent agent,
        PickUper pickUper) : base(stateMachine, false)
    {
        _animator = animator;
        _objectDetector = objectDetector;
        _agent = agent;
        _pickUper = pickUper;

        _delay = new WaitForSeconds(_updateTime);
    }

    public override void Enter()
    {
        _hasObject = false;
        _thrownObject = null;
        _agent.isStopped = false;
        FindObject();
    }

    public override void Exit()
    {
        _hasObject = false;
        _agent.isStopped = true;
    }

    private void FindObject()
    {
        _thrownObject = _objectDetector.GetNearestResource();

        if (_thrownObject != null)
        {
            StateMachine.StartCoroutine(PickUpRoutine());
        }
        else
        {
            SetCanExit(true);
            StateMachine.OnLostPlayer();
        }
    }

    public override IEnumerator UpdateRoutine()
    {
        while (_hasObject == false)
            yield return _delay;

        StateMachine.OnReadyToThrow(_thrownObject);
        SetCanExit(true);
    }

    private IEnumerator PickUpRoutine()
    {
        float treshold = 1f;
        _agent.destination = _thrownObject.position;
        _animator.UpdateSpeed(StateMachine.Config.MoveConfig.MoveSpeed);

        while (_agent.pathPending || _agent.remainingDistance > treshold)
        {
            RotateTo(_thrownObject.position);
            yield return _delay;
        }

        _agent.isStopped = true;
        _animator.PlayPickUp();

        while (_animator.IsPicked == false)
        {
            yield return _delay;
        }

        _pickUper.Pickup(_thrownObject);
        _hasObject = true;
    }
}

