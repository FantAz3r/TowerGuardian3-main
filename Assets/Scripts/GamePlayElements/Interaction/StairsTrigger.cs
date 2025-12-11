using System;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public event Action Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Entered?.Invoke();
        }
    }
}
