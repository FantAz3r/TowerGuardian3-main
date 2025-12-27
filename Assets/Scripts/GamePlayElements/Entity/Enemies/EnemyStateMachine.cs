using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;

    private Mover _mover;
    private Rotator _rotator;
    private AttackZone _attackZone;
    private IEnemyState _currentState;
    private EnemyAnimator _animator;
    private NavMeshAgent _agent;
    private Transform _player;
    private PickUper _picker;
    private Health _health;
    private Collider _collider;

    private Coroutine _currentCoroutine;

    private ThrownObjectDetector _objectDetector;
    private TargetDetector _targetDetector;
    private AttackDetector _attackDetector;

    private Dictionary<StateType, State> _states = new Dictionary<StateType, State>();

    public event Action<IEnemyState> StateChanged;

    public Transform Target { get; private set; }
    public Mover Mover => _mover;
    public Rotator Rotator => _rotator;
    public AttackZone AttackZone => _attackZone;
    public EnemyConfig Config => _config;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _rotator = GetComponentInChildren<Rotator>();
        _attackZone = GetComponentInChildren<AttackZone>();
        _animator = GetComponentInChildren<EnemyAnimator>();
        _agent = GetComponent<NavMeshAgent>();
        _targetDetector = GetComponentInChildren<TargetDetector>();
        _attackDetector = GetComponentInChildren<AttackDetector>();
        _objectDetector = GetComponentInChildren<ThrownObjectDetector>();
        _picker = GetComponentInChildren<PickUper>();
        _collider = GetComponent<Collider>();
    }

    public void Init(Transform player)
    {
        _player = player;
        _collider.enabled = true;

        ActivateStates();
        _targetDetector.PlayerDetected += OnSeePlayer;
        _targetDetector.PlayerLost += OnLostPlayer;
        _attackDetector.PlayerDetected += OnPlayerInMeleeRange;
        _attackDetector.PlayerLost += OnChasePlayer;
        _health.Died += OnDie;

        if (_states.ContainsKey(StateType.Patrol))
        {
            SetState(_states[StateType.Patrol]);
        }

    }

    private void OnSeePlayer()
    {
        int random = UnityEngine.Random.Range(0, 2);

        if (random == 0 || _states.ContainsKey(StateType.FindObject) == false)
        {
            SetState(_states[StateType.Chase]);
        }
        else
        {
            SetState(_states[StateType.FindObject]);
        }
    }

    public void OnDie()
    {
        _collider.enabled = false;

        StopCoroutine(_currentCoroutine);
        _currentCoroutine = null;
        _currentState?.Exit();

        _targetDetector.PlayerDetected -= OnSeePlayer;
        _targetDetector.PlayerLost -= OnLostPlayer;
        _attackDetector.PlayerDetected -= OnPlayerInMeleeRange;
        _attackDetector.PlayerLost -= OnChasePlayer;
        _health.Died -= OnDie;
    }

    public void OnLostPlayer()
    {
        SetState(_states[StateType.Patrol]);
    }

    private void OnPlayerInMeleeRange()
    {
        SetState(_states[StateType.Attack]);
    }

    public void OnChasePlayer()
    {
        SetState(_states[StateType.Chase]);
    }

    public void OnReadyToThrow(Transform throwObject)
    {
        ThrowState throwState = _states[StateType.Thrown] as ThrowState;
        throwState.SetThrownObject(throwObject);
        SetState(_states[StateType.Thrown]);
    }

    private void SetState(IEnemyState newState)
    {
        if (_currentState == newState)
            return;

        StartCoroutine(SwitchState(newState));
    }

    private IEnumerator SwitchState(IEnemyState newState)
    {
        if (_currentCoroutine != null)
        {
            while (_currentState.CanExit == false)
            {
                yield return null;
            }

            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentState?.Exit();
        _currentState = newState;
        StateChanged?.Invoke(_currentState);
        _currentState.Enter();

        _currentCoroutine = StartCoroutine(_currentState.UpdateRoutine());
    }

    private void ActivateStates()
    {
        _states.Clear();

        foreach (var stateType in _config.AllowedStates)
        {
            switch (stateType)
            {
                case StateType.Patrol:
                    _states.Add(StateType.Patrol, new PatrolState(this, _agent, _animator));
                    break;
                case StateType.Chase:
                    _states.Add(StateType.Chase, new ChaseState(this, _agent, _animator, _player));
                    break;
                case StateType.Attack:
                    _states.Add(StateType.Attack, new AttackState(this, _animator, _player));
                    break;
                case StateType.Thrown:
                    _states.Add(StateType.Thrown, new ThrowState(this, _animator, _player));
                    break;
                case StateType.FindObject:
                    _states.Add(StateType.FindObject, new PickupState(this, _animator, _objectDetector, _agent, _picker));
                    break;
            }
        }
    }
}