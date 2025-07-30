using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConfig _config;
    private TargetDetector _targetDetector;
    private Mover _mover;
    private Rotator _rotator;
    private AttackZone _attackZone;
    private IEnemyState _currentState;

    private Coroutine _stateCoroutine;
    private WaitForSeconds _wait;
    private float _waitTime = 0.2f;
    public Transform Target { get; private set; }
    public Mover Mover => _mover;
    public Rotator Rotator => _rotator;
    public AttackZone AttackZone => _attackZone;

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitTime);
        _targetDetector = GetComponentInChildren<TargetDetector>();
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _attackZone = GetComponentInChildren<AttackZone>();
    }

    private void Start()
    {
        SetState(new PatrolState());
        _stateCoroutine = StartCoroutine(StateRoutine());
    }

    private IEnumerator StateRoutine()
    {
        while (enabled)
        {
            if (_targetDetector.GetTarget() != null)
            {

                if ((_currentState is ChaseState) == false)
                {
                    SetState(new ChaseState(_targetDetector.GetTarget()));
                }
            }
            else
            {
                Target = null;

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
        _currentState.Enter(this);
    }
}