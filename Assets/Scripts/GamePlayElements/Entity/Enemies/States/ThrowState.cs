using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowState : State
{
    private EnemyAnimator _animator;
    private Transform _player;
    private Transform _thrownObject;
    private TargetDetector _targetDetector;
    private ISpawnerService _spawnerService;
    private bool isThrowing = false;

    public ThrowState(
        EnemyStateMachine stateMachine,
        EnemyAnimator animator,
        Transform player,
        TargetDetector targetDetector
        ) : base(stateMachine, false)
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
        _animator = animator;
        _player = player;
        _targetDetector = targetDetector;
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
            StateMachine.OnChasePlayer();
            yield break;
        }
        else
        {
            yield return ThrownRoutine();
        }

        SetCanExit(true);
        StateMachine.OnChasePlayer();
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
        ThrownObject thrownObject = _thrownObject.AddComponent<ThrownObject>();
        thrownObject.StartFly(StateMachine.Config.ThrowDamage, _player.position);
        _spawnerService.SendEffectReqest(EffectType.AimPoint, _player.position );

        isThrowing = false;
    }

    public override void Exit()
    {
        isThrowing = false;
        _thrownObject = null;
        _targetDetector.gameObject.SetActive( true);
        SetCanExit(true);
    }
}
