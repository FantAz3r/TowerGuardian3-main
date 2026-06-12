using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField]
        private float _speedMultiplier = 1f;
        [SerializeField]
        private float _smoothTime = 0.05f;

        private Animator _animator;
        private Health _health;

        private float _currentSpeed;
        private float _velSpeed;
        private int _hashAttackSpeedMultiplayer;
        private int _hashSpeed;
        private int _hashAttack;
        private int _hashPickUp;
        private int _hashThrow;
        private int _hashJump;
        private int _hashDie;
        private int _hashRandom;
        private int _hashUltimateAttack;
        private int _hashThornsAttack;
        private int _hashEndUltimate;

        private List<AnimationClip> _attackClips;

        public event Action Attacked;

        public event Action Grounded;

        public event Action Throwed;

        public event Action ThornAttacked;

        public bool IsThrowing { get; private set; }

        public bool IsPicked { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponentInParent<Health>();

            _hashRandom = Animator.StringToHash("Random");
            _hashSpeed = Animator.StringToHash("Speed");
            _hashAttack = Animator.StringToHash("Attack");
            _hashPickUp = Animator.StringToHash("Pick");
            _hashThrow = Animator.StringToHash("Throw");
            _hashJump = Animator.StringToHash("Jump");
            _hashDie = Animator.StringToHash("Die");
            _hashUltimateAttack = Animator.StringToHash("LavaBallAttack");
            _hashEndUltimate = Animator.StringToHash("EndUltimate");
            _hashAttackSpeedMultiplayer = Animator.StringToHash("AttackSpeed");
            _hashThornsAttack = Animator.StringToHash("IsThornAttack");
            _attackClips = GetAnimationClipsContaining("Hited");
        }

        private void OnEnable()
        {
            _health.Died += PlayDie;
        }

        private void OnDisable()
        {
            _health.Died -= PlayDie;
        }

        public void UpdateSpeed(float speed)
        {
            IsThrowing = false;
            float targetSpeed = speed * _speedMultiplier;
            _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _velSpeed, _smoothTime);
            _animator.SetFloat(_hashSpeed, _currentSpeed);
        }

        public void PlayAttack(float attackTime = 1f)
        {
            int random = Random.Range(0, _attackClips.Count);
            _animator.SetInteger(_hashRandom, random);
            _animator.SetFloat(_hashAttackSpeedMultiplayer, _attackClips[random].length / attackTime);
            _animator.SetBool(_hashAttack, true);
        }

        public void SuspendAttack()
        {
            _animator.SetBool(_hashAttack, false);
        }

        public void PlayPickUp()
        {
            _animator.SetTrigger(_hashPickUp);
        }

        public void PlayThrow()
        {
            _animator.SetTrigger(_hashThrow);
        }

        public void PlayJump()
        {
            _animator.SetTrigger(_hashJump);
        }

        public void PlayDie()
        {
            _animator.SetTrigger(_hashDie);
        }

        public void PlaytUtlimate()
        {
            _animator.SetTrigger(_hashUltimateAttack);
            _animator.ResetTrigger(_hashEndUltimate);
        }

        public void PlayEndUltimate()
        {
            _animator.SetTrigger(_hashEndUltimate);
        }

        public void PlayThornsAttack()
        {
            _animator.SetBool(_hashThornsAttack, true);
        }

        public void OnAnimationAttack()
        {
            Attacked?.Invoke();
        }

        public void OnAnimationThrow()
        {
            Throwed?.Invoke();
            IsThrowing = true;
        }

        public void OnAnimationPicked()
        {
            IsPicked = true;
        }

        public void OnAnimationDie()
        {
            _health.Die();
        }

        public void OnAimationThornsAttack()
        {
            _animator.SetBool(_hashThornsAttack, false);
            ThornAttacked?.Invoke();
        }

        public void OnAnimationJump()
        {
            Grounded?.Invoke();
            _animator.ResetTrigger(_hashJump);
        }

        private List<AnimationClip> GetAnimationClipsContaining(string partialName)
        {
            List<AnimationClip> clips = new List<AnimationClip>();

            foreach (var clip in _animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name.Contains(partialName))
                {
                    clips.Add(clip);
                }
            }

            return clips;
        }
    }
}