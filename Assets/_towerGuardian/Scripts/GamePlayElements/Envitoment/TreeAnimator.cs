using UnityEngine;

public class TreeAnimator : MonoBehaviour
{
    private Health _health;
    private Animator _animator;

    private int _hashHited;
    private int _hashDied;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _hashHited = Animator.StringToHash("Hited");
        _hashDied = Animator.StringToHash("Die");
    }

    private void OnEnable()
    {
        _health.IsValueChange += OnHited;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.IsValueChange -= OnHited;
    }

    private void OnHited(float useles1, float useles2)
    {
        _animator.SetTrigger(_hashHited);
    }

    private void OnDied()
    {
        _animator.SetTrigger(_hashDied);
    }
}
