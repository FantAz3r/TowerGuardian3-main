using System;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    public event Action PlayerDetected;
    public event Action PlayerLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            PlayerDetected?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            PlayerLost?.Invoke();
        }
    }
}
