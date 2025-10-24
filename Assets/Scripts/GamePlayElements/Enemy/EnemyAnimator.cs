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
        _hashX = Animator.StringToHash("X");
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
        float x = 0f;
        float y = 0f;
        float dampTime = 0.05f;
        float trashhold = 0.001f;

        Vector2 lookDirection = _rotator.CurrentDirection.normalized;
        Vector2 moveDirection = _mover.Direction.normalized;

        float moveSpeed = _mover.Direction.SqrMagnitude();

        if (moveSpeed > trashhold)
        {
            float angleDifference = Vector2.SignedAngle(lookDirection, moveDirection) * Mathf.Deg2Rad;
            x = Mathf.Sin(angleDifference);
            y = Mathf.Cos(angleDifference);
        }


        float targetSpeed = moveSpeed * _speedMultiplier;
        _currentSpeed = Mathf.SmoothDamp(_currentSpeed, targetSpeed, ref _velSpeed, _smoothTime);

        _animator.SetFloat(_hashX, x, dampTime, Time.deltaTime);
        _animator.SetFloat(_hashY, y, dampTime, Time.deltaTime);
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
