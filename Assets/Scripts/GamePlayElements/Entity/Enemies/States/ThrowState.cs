using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowState : State
{
    private EnemyStateMachine _stateMachine;
    private EnemyAnimator _animator;
    private Transform _player;
    private Transform _thrownObject;
    private bool isThrowing = false;

    public ThrowState(
        EnemyStateMachine stateMachine,
        EnemyAnimator animator,
        Transform player
        ) : base(stateMachine, false)
    {
        _stateMachine = stateMachine;
        _animator = animator;
        _player = player;
    }

    public override void Enter()
    {
        isThrowing = true;
    }

    public void SetThrownObject(Transform thrownObject)
    {
        _thrownObject = thrownObject;
    }

    public override IEnumerator UpdateRoutine()
    {
        if (_thrownObject == null)
        {
            SetCanExit(true);
            _stateMachine.OnChasePlayer();
            yield break;
        }
        else
        {
            yield return ThrownRoutine();
        }

        SetCanExit(true);
        _stateMachine.OnChasePlayer();
    }

    private IEnumerator ThrownRoutine()
    {
        _animator.Throwed += OnThrow;
        RotateTo(_player.transform.position);
        _animator.PlayThrow();

        while (isThrowing)
        {
            yield return null;
        }

        _animator.Throwed -= OnThrow;
        _thrownObject = null;
    }

    private void OnThrow()
    {
        var rigidbody = _thrownObject.GetComponent<Rigidbody>();
        _thrownObject.transform.parent = null;

        if (rigidbody)
        {
            Vector3 velocity = Utils.CalculateThrowForce(_stateMachine.transform.position, _player.position);

            if (velocity != Vector3.zero)
            {
                rigidbody.isKinematic = false;
                rigidbody.AddForce(velocity, ForceMode.VelocityChange);
            }
        }

        ThrownObject thrownObject = _thrownObject.AddComponent<ThrownObject>();
        thrownObject.StartFly(_stateMachine.Config.ThrowDamage);

        isThrowing = false;
    }

    public override void Exit()
    {
        isThrowing = false;
        _thrownObject = null;
    }
}
