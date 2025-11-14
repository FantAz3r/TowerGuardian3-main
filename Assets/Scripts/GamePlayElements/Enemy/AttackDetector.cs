using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    private EnemyStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = GetComponentInParent<EnemyStateMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _stateMachine.SetAttackState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _stateMachine.SetChaseState();
        }
    }
}
