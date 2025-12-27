using System;
using UnityEngine;

public class StairsTrigger : MonoBehaviour
{
    public event Action Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Debug.Log(gameObject.name);
            Entered?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Debug.Log(gameObject.GetHashCode());

        }
    }
}
