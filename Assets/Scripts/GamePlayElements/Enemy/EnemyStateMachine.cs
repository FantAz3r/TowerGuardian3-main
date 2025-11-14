using System;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;

    private Mover _mover;
    private Rotator _rotator;
    private AttackZone _attackZone;
    private IEnemyState _currentState;
    private ISpawnerService _spawnerService;

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
    }

    public void Init(ISpawnerService spawnerService)
    {
        _spawnerService = spawnerService;
        SetState(new PatrolState(this));
    }

    public void SetChaseState(Player player)
    {
        SetState(new ChaseState(player, this, _spawnerService));
    }

    public void SetPatrolState(Player player)
    {
        SetState(new PatrolState(this));
    }

    private void SetState(IEnemyState newState)
    {
        Debug.Log(newState);
        _currentState?.Exit();
        _currentState = newState;
        StateChanged?.Invoke(_currentState);
        _currentState.Enter(this);
    }
}