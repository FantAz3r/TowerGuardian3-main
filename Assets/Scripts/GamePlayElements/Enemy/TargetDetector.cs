using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = GetComponentInParent<EnemyStateMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _stateMachine.SetChaseState(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _stateMachine.SetPatrolState(player);
        }
    }
}
