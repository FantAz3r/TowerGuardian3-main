using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private float _smoothTime = 0.05f;

    private Animator _animator;

    private float _currentSpeed;
    private float _velSpeed;
    private int _hashSpeed;
    private int _hashAttack;
    private int _hashPickUp;
    private int _hashThrow;
    private int _hashJump;

    public event Action Attacked;
    public event Action Throwed;

    public bool IsThrowing { get; private set; }
    public bool IsPicked { get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _hashSpeed = Animator.StringToHash("Speed");
        _hashAttack = Animator.StringToHash("Attack");
        _hashPickUp = Animator.StringToHash("Pick");
        _hashThrow = Animator.StringToHash("Throw");
        _hashJump = Animator.StringToHash("Jump");
    }

    public void UpdateSpeed(float speed)
    {
        IsThrowing = false;
        float targetSpeed = speed * _speedMultiplier;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _velSpeed, _smoothTime);
        _animator.SetFloat(_hashSpeed, _currentSpeed);
    }

    public void PlayAttack()
    {
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
}
