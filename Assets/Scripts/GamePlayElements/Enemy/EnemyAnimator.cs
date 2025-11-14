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

    public event Action Attacked;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _hashSpeed = Animator.StringToHash("Speed");
        _hashAttack = Animator.StringToHash("Attack");
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(_hashAttack);
        Attacked?.Invoke();
    }
}
