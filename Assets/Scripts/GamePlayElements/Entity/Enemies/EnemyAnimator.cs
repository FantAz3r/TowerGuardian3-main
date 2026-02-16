using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private float _smoothTime = 0.05f;

    private Animator _animator;
    private Health _health;

    private float _currentSpeed;
    private float _velSpeed;
    private int _hashSpeed;
    private int _hashAttack;
    private int _hashPickUp;
    private int _hashThrow;
    private int _hashJump;
    private int _hashDie;

    public event Action Attacked;
    public event Action Throwed;

    public bool IsThrowing { get; private set; }
    public bool IsPicked { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponentInParent<Health>();

        _hashSpeed = Animator.StringToHash("Speed");
        _hashAttack = Animator.StringToHash("Attack");
        _hashPickUp = Animator.StringToHash("Pick");
        _hashThrow = Animator.StringToHash("Throw");
        _hashJump = Animator.StringToHash("Jump");
        _hashDie = Animator.StringToHash("Die");
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
        AnimationClip attackClip = GetAnimationClip("Attack");

        if (attackClip == null)
        {
            _animator.speed = 1f;
            _animator.SetBool(_hashAttack, true);
            return;
        }

        float clipLength = attackClip.length;
        float speed = clipLength / attackTime;
        _animator.speed = speed;
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

    private AnimationClip GetAnimationClip(string clipName)
    {
        foreach (var clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip;
        }
        return null;
    }
}
