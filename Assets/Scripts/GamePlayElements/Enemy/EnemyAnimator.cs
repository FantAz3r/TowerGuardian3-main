using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private float _smoothTime = 0.05f;

    private Animator _animator;
    private Mover _mover;
    private EnemyStateMachine _stateMashine;

    private float _currentSpeed;
    private float _velSpeed;
    private int _hashSpeed;
    private int _hashAttack;

    private IEnemyState _state;

    private void Awake()
    {
        _mover = GetComponentInParent<Mover>();
        _stateMashine = GetComponentInParent<EnemyStateMachine>();
        _animator = GetComponent<Animator>();

        _hashSpeed = Animator.StringToHash("Speed");
        _hashAttack = Animator.StringToHash("Attack");
    }

    private void OnEnable()
    {
        _stateMashine.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _stateMashine.StateChanged -= OnStateChanged;
    }

    private void Update()
    {
        UpdateMovementAnimation();
    }

    private void UpdateMovementAnimation()
    {
        float moveSpeed = _mover.Direction.sqrMagnitude;
        float targetSpeed = moveSpeed * _speedMultiplier;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _velSpeed, _smoothTime);

        _animator.SetFloat(_hashSpeed, _currentSpeed);
    }

    private void OnStateChanged(IEnemyState state)
    {
        _state = state;

    }

    public void AnimationAttack()
    {
        _animator.SetTrigger(_hashAttack);
    }
}
