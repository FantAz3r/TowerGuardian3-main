using TowerGuardian.Scripts.Infrastructure;
using TowerGuardian.Scripts.Infrastructure.Servises.Interfaces;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies.States
{
    public class AttackState : State
    {
        private Health _target;
        private ISpawnerService _spawnerService;
        private float _timeSinceLastAttack;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            _target = Enemy.Target?.GetComponent<Health>();
            _spawnerService = ServiceLocator.Get<ISpawnerService>();

            Enemy.AnimationAnimator.Attacked += OnAnimAttackHit;

            _timeSinceLastAttack = Enemy.Config.AttackCooldown;
            Enemy.Agent.IsStopAgent(true);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_target == null && !_target.IsAlive)
            {
                return;
            }

            RotateTo(_target.transform.position);
            _timeSinceLastAttack += Time.deltaTime;

            if (_timeSinceLastAttack >= Enemy.Config.AttackCooldown)
            {
                Enemy.AnimationAnimator.PlayAttack(Enemy.Config.AttackCooldown);
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
            _target.TakeDamage(Enemy.Config.GetDamage());
        }
    }
}