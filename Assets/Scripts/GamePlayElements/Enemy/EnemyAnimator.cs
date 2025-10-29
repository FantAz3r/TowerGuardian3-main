using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float _speedMultiplier = 1f;
    [SerializeField] private float _smoothTime = 0.05f;

    private Animator _animator;
    private Mover _mover;
    private Rotator _rotator;
    private EnemyStateMachine _enemy;
    private float _currentSpeed;
    private float _velSpeed;
    private int _hashX;
    private int _hashY;
    private ChaseState _attackState;

    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Awake()
    {
        _mover = GetComponentInParent<Mover>();
        _rotator = GetComponent<Rotator>();
        _enemy = GetComponentInParent<EnemyStateMachine>();
        _animator = GetComponent<Animator>();
        _hashX = Animator.StringToHash("Speed");
        _hashY = Animator.StringToHash("Y");
    }

    private void OnEnable()
    {
        _enemy.StateChanged += TryAttackAnimation;
    }

    private void OnDisable()
    {
        _enemy.StateChanged -= TryAttackAnimation;
    }

    private void Update()
    {
        UpdateMovementParameters();
    }

    private void UpdateMovementParameters()
    {
        float dampTime = 0.05f;
        float moveSpeed = _mover.Direction.SqrMagnitude();

        float targetSpeed = moveSpeed * _speedMultiplier;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _velSpeed, _smoothTime);

        _animator.SetFloat(_hashX, moveSpeed, dampTime, Time.deltaTime);
    }

    private void TryAttackAnimation(IEnemyState state)
    {
        if(state is ChaseState)
        {
            _attackState = state as ChaseState;
            _attackState.Attacked += TriggerAttack;
        }
        else
        {
            if(_attackState == null)
                return;

            _attackState.Attacked -= TriggerAttack;
        }
    }

    public void ApplyDamage()
    {
        _attackState.ApplyDamage();
    }

    public void TriggerAttack()
    {
        _animator.SetTrigger(Attack);
    }
}
