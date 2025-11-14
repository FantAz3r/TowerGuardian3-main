using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;

    private Mover _mover;
    private Rotator _rotator;
    private AttackZone _attackZone;
    private IEnemyState _currentState;
    private ISpawnerService _spawnerService;
    private EnemyAnimator _animator;

    private Dictionary<StateType, State> _states = new Dictionary<StateType, State>();

    public event Action<IEnemyState> StateChanged;
    public Transform Target { get; private set; }
    public Mover Mover => _mover;
    public Rotator Rotator => _rotator;
    public AttackZone AttackZone => _attackZone;
    public EnemyConfig Config => _config;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _rotator = GetComponentInChildren<Rotator>();
        _attackZone = GetComponentInChildren<AttackZone>();
        _animator = GetComponentInChildren<EnemyAnimator>();

        _states.Add(StateType.Patrol, new PatrolState(this, _animator));
        _states.Add(StateType.Chase, new ChaseState(this, _animator, Target));
        _states.Add(StateType.Attack, new AttackState(this, _animator, _spawnerService, Target));
    }

    public void Init(ISpawnerService spawnerService)
    {
        _spawnerService = spawnerService;
        SetState(_states[StateType.Patrol]);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void SetChaseState()
    {
        SetState(_states[StateType.Chase]);
    }

    public void SetPatrolState()
    {
        SetState(_states[StateType.Patrol]);
    }

    public void SetAttackState()
    {
        SetState(_states[StateType.Attack]);
    }

    private void SetState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        StateChanged?.Invoke(_currentState);
        _currentState.Enter();
    }
}