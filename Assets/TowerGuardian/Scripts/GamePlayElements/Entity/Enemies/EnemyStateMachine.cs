using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private const int MaxRandomValue = 4;
        private const int MinRandomValue = 0;

        [SerializeField]
        private Enemy _enemy;
        private float _attackRangeTreshold = 1.1f;
        private float _abilityCooldown;
        private float _ultimateCooldown;

        private int _hashIsSeeTarget;
        private int _hashTargetInAttackRange;
        private int _hashRandom;
        private int _hashHasObject;
        private int _hashPick;
        private int _hashThrowEnded;
        private int _hashUltimateCooldown;
        private int _hashIsUltimateActive;
        private int _hashHealth;
        private int _hashIsThornAttack;
        private int _hashCooldown;

        private void Awake()
        {
            _hashIsSeeTarget = Animator.StringToHash("SeeTarget");
            _hashTargetInAttackRange = Animator.StringToHash("TargetInAttackRange");
            _hashRandom = Animator.StringToHash("Random");
            _hashHasObject = Animator.StringToHash("HasObject");
            _hashPick = Animator.StringToHash("IsPickup");
            _hashThrowEnded = Animator.StringToHash("ThrowEnded");
            _hashHealth = Animator.StringToHash("Health");
            _hashIsUltimateActive = Animator.StringToHash("IsUltimateActive");
            _hashUltimateCooldown = Animator.StringToHash("UltimateCooldown");
            _hashIsThornAttack = Animator.StringToHash("IsThornAttack");
            _hashCooldown = Animator.StringToHash("Cooldown");
        }

        private void Update()
        {
            if (HasParameter(_hashRandom))
            {
                _enemy.BehaviorAnimator.SetInteger(_hashRandom, Random.Range(MinRandomValue, MaxRandomValue));
            }

            if (HasParameter(_hashHealth))
            {
                _enemy.BehaviorAnimator.SetFloat(_hashHealth, _enemy.Health.CurrentHealth / _enemy.Health.MaxHealth);
            }

            UpdateTimer();

            if (_enemy.Target == null)
            {
                return;
            }

            float sqrDistance = (transform.position - _enemy.Target.transform.position).sqrMagnitude;

            if (sqrDistance <= _enemy.Config.AttackRange * _enemy.Config.AttackRange)
            {
                _enemy.BehaviorAnimator.SetBool(_hashTargetInAttackRange, true);
            }
            else if (sqrDistance >= _enemy.Config.AttackRange * _enemy.Config.AttackRange * _attackRangeTreshold)
            {
                _enemy.BehaviorAnimator.SetBool(_hashTargetInAttackRange, false);
            }

            if (sqrDistance <= _enemy.Config.DetectionRadius * _enemy.Config.DetectionRadius)
            {
                _enemy.BehaviorAnimator.SetBool(_hashIsSeeTarget, true);
            }
            else
            {
                _enemy.BehaviorAnimator.SetBool(_hashIsSeeTarget, false);
            }
        }

        public void Init()
        {
            _enemy.Agent.EnableAgent(true);
            _enemy.Collider.enabled = true;
            _enemy.Rotator.CanRotate(true);

            _enemy.TargetDetector.TargetDetected += OnSeeTarget;
            _enemy.TargetDetector.TargetLost += OnLostTarget;

            _enemy.Health.Died += OnDie;
        }

        public void SetCooldown(float cooldown)
        {
            _abilityCooldown = cooldown;
            _enemy.BehaviorAnimator.SetFloat(_hashCooldown, _abilityCooldown);
        }

        public void SetUltimateCooldown(float cooldown)
        {
            _ultimateCooldown = cooldown;
            _enemy.BehaviorAnimator.SetFloat(_hashUltimateCooldown, _ultimateCooldown);
        }

        private void OnSeeTarget(Transform target)
        {
            _enemy.SetNewTarget(target);
        }

        private void OnLostTarget(Transform target)
        {
            _enemy.SetNewTarget(target);
        }

        public void OnDie()
        {
            _enemy.Collider.enabled = false;
            _enemy.Agent.EnableAgent(false);
            _enemy.Rotator.CanRotate(false);
            _enemy.Health.Died -= OnDie;
        }

        public void OnReadyToThrow()
        {
            _enemy.BehaviorAnimator.SetBool(_hashPick, false);
            _enemy.BehaviorAnimator.SetBool(_hashHasObject, true);
        }

        public void OnNullTrownObject()
        {
            _enemy.BehaviorAnimator.SetBool(_hashHasObject, false);
        }

        public void OnStartPickup()
        {
            _enemy.BehaviorAnimator.SetBool(_hashPick, true);
        }

        public void StartThornsAttack()
        {
            _enemy.BehaviorAnimator.SetBool(_hashIsThornAttack, true);
        }

        public void OnEndThornAttack()
        {
            _enemy.BehaviorAnimator.SetBool(_hashIsThornAttack, false);
        }

        public void StatrUltimate()
        {
            _enemy.BehaviorAnimator.SetBool(_hashIsUltimateActive, true);
        }

        public void EndUltimate()
        {
            _enemy.BehaviorAnimator.SetBool(_hashIsUltimateActive, false);
        }

        public void OnStopPickup()
        {
            _enemy.BehaviorAnimator.SetBool(_hashPick, false);
        }

        public void OnThrowEnded()
        {
            _enemy.BehaviorAnimator.SetTrigger(_hashThrowEnded);
        }

        private void UpdateTimer()
        {
            if (HasParameter(_hashCooldown))
            {
                if (_abilityCooldown > 0)
                {
                    _abilityCooldown -= Time.deltaTime;
                    _enemy.BehaviorAnimator.SetFloat(_hashCooldown, _abilityCooldown);
                }
            }

            if (HasParameter(_hashUltimateCooldown))
            {
                if (_ultimateCooldown > 0)
                {
                    _ultimateCooldown -= Time.deltaTime;
                    _enemy.BehaviorAnimator.SetFloat(_hashUltimateCooldown, _ultimateCooldown);
                }
            }
        }

        private bool HasParameter(int hash)
        {
            foreach (AnimatorControllerParameter param in _enemy.BehaviorAnimator.parameters)
            {
                if (Animator.StringToHash(param.name) == hash)
                {
                    return true;
                }
            }

            return false;
        }
    }
}