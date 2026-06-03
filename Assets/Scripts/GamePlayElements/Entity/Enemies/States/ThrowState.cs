using UnityEngine;

public class ThrowState : State
{
    private ISpawnerService _spawnerService;
    private bool _isThrowing;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _spawnerService = ServiceLocator.Get<ISpawnerService>();

        Enemy.Agent.IsStopAgent(true);
        _isThrowing = true;
        Enemy.AnimationAnimator.Throwed += OnThrow;

        Enemy.AnimationAnimator.PlayThrow();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Enemy == null || Enemy.Target == null) return;

        if (_isThrowing)
        {
            RotateTo(Enemy.Target.position);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Enemy != null)
        {
            Enemy.AnimationAnimator.Throwed -= OnThrow;
            Enemy.TargetDetector.gameObject.SetActive(true);
            Enemy.SetThrownObject(null);
        }

        _isThrowing = false;
        Enemy.Agent.IsStopAgent(false);
    }

    private void OnThrow()
    {
        _isThrowing = false;
        Enemy.StateMachine.OnThrowEnded();
        if (Enemy.ThrownObject == null) return;

        ThrownObject thrownObject = Enemy.ThrownObject.gameObject.AddComponent<ThrownObject>();
        thrownObject.StartFly(Enemy.Config.ThrowDamage, Enemy.Target.position);
        
        _spawnerService.SendEffectReqest(EffectType.AimPoint, Enemy.Target.position);
    }
}
