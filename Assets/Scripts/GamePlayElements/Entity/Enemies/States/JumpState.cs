using UnityEngine;

public class JumpState : State
{
    private const float JumpAttackCooldown = 9;
    private const float _rangeMultipier = 0.8f;
    private ISpawnerService _spawnerService;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();

        base.OnStateEnter(animator, stateInfo, layerIndex);
        Enemy.AnimationAnimator.PlayJump();
        Enemy.AnimationAnimator.Grounded += OnGrounded;

        Enemy.StateMachine.SetCooldown(JumpAttackCooldown);
        Enemy.Agent.SetDestination(Enemy.Target.position);
        Enemy.Agent.SetMoveSpeed(Enemy.Config.MoveConfig.MoveSpeed * 5);
        _spawnerService.SendSoundReqest(Enemy.Config.JumpSound, Enemy.transform.position);
    }

    private void OnGrounded()
    {
        _spawnerService.SendEffectReqest(EffectType.Bounce, Enemy.transform.position);
        Enemy.Agent.SetMoveSpeed(Enemy.Config.MoveConfig.MoveSpeed);
        Enemy.AnimationAnimator.Grounded -= OnGrounded;

        Collider[] hits = Physics.OverlapSphere(Enemy.transform.position, Enemy.Config.AttackRange * _rangeMultipier);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Health demadeable))
            {
                if (demadeable == Enemy.Health)
                {
                    continue; 
                }

                demadeable.TakeDamage(Enemy.Config.JumpDamage);
            }
        }
    }
}