using UnityEngine;

public class AttackStateBehaviour : State
{
    private Player _player;
    private ISpawnerService _spawnerService;
    private float _timeSinceLastAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _player = Enemy.Target?.GetComponent<Player>();
        _spawnerService = ServiceLocator.Get<ISpawnerService>();

        Enemy.AnimationAnimator.Attacked += OnAnimAttackHit;

        _timeSinceLastAttack = Enemy.Config.AttackCooldown; 
        Enemy.Agent.IsStopAgent(true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player == null && _player.IsAlive == false) return;

        RotateTo(_player.transform.position);
        _timeSinceLastAttack += Time.deltaTime;

        if (_timeSinceLastAttack >= Enemy.Config.AttackCooldown)
        {
            Enemy.AnimationAnimator.PlayAttack();
            _timeSinceLastAttack = 0f;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enemy.AnimationAnimator.Attacked -= OnAnimAttackHit;
        Enemy.AnimationAnimator.SuspendAttack();
        Enemy.Agent.IsStopAgent(false);

    }

    private void OnAnimAttackHit()
    {
        _spawnerService.SendSoundReqest(Enemy.Config.HitSound, Enemy.transform.position);
        _player.Health.TakeDamage(Enemy.Config.GetDamage());
    }
}