using System;
using System.Collections;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;

    private TargetDetector _targetDetector;
    private Mover _mover;
    private Rotator _rotator;
    private AttackZone _attackZone;
    private IEnemyState _currentState;

    private WaitForSeconds _wait;
    private float _waitTime = 0.2f;

    public event Action<IEnemyState> StateChanged;
    public Transform Target { get; private set; }
    public Mover Mover => _mover;
    public Rotator Rotator => _rotator;
    public AttackZone AttackZone => _attackZone;
    public EnemyConfig Config => _config;

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitTime);
        _targetDetector = GetComponentInChildren<TargetDetector>();
        _mover = GetComponent<Mover>();
        _rotator = GetComponentInChildren<Rotator>();
        _attackZone = GetComponentInChildren<AttackZone>();
    }

    private void OnEnable()
    {
        StartCoroutine(StateRoutine());
    }

    private void OnDisable()
    {
        _currentState?.Exit();
        StopCoroutine(StateRoutine());
    }

    private IEnumerator StateRoutine()
    {
        yield return _wait;

        while (enabled)
        {
            Player player = _targetDetector.GetTarget();
            Target = player != null ? player.transform : null;

            if (player != null)
            {
                if ((_currentState is ChaseState) == false)
                {
                    SetState(new ChaseState(player, this));
                }
            }
            else
            {
                if ((_currentState is PatrolState) == false)
                {
                    SetState(new PatrolState());
                }
            }

            _currentState?.Update();
            yield return _wait;
        }
    }


    private void SetState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        StateChanged?.Invoke(_currentState);
        _currentState.Enter(this);
    }
}