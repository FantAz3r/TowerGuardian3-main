using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour, IBuffble
{
    [SerializeField] private NavMeshAgent _agent;
    private StatsCalculator _statsCalculator;
    private float _startSpeed;

    private void Awake()
    {
        _statsCalculator = new StatsCalculator();
    }

    public void SetMoveSpeed(float speed)
    {
        _startSpeed = speed;
        _agent.speed = _startSpeed;
    }

    public void SetAngularSpeed(float angularSpeed) => _agent.angularSpeed = angularSpeed;

    public void SetDestination(Vector3 point)
    {
        if (_agent.isActiveAndEnabled)
        {
            _agent.SetDestination(point);
        }
    }

    public float GetRemainingDistance()
    {
        if (_agent.isActiveAndEnabled)
        {
           return _agent.remainingDistance;
        }

        return 0;
    }

    public bool GetPathPedding() => _agent.pathPending;

    public void EnableAgent(bool isEnable) => _agent.enabled = isEnable;
    
    public void IsStopAgent(bool isStop)
    {
        if (_agent.isActiveAndEnabled)
        {
            _agent.isStopped = isStop;
        }
    }

    public void EnableBuff()
    {
        
    }

    public void ApplyBuff(IEffect effect)
    {
        _statsCalculator.AddEffect(effect);
        _agent.speed = _statsCalculator.Calculate(_startSpeed);
    }

    public void Recalculate()
    {
        _agent.speed = _statsCalculator.Calculate(_startSpeed);
    }

    public void RemoveBuff(IEffect effect)
    {
        _statsCalculator.RemoveEffect(effect);
        _agent.speed = _statsCalculator.Calculate(_startSpeed);
    }
}
