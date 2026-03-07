using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public bool HasTarget { get; private set; } = false;

    public event Action PlayerDetected;
    public event Action PlayerLost;

    private void OnTriggerEnter(Collider other)
    {
        HasTarget = true;

        if (other.TryGetComponent<Player>(out _))
        {
            PlayerDetected?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HasTarget = false;

        if (other.TryGetComponent<Player>(out _))
        {
            PlayerLost?.Invoke();
        }
    }
}
