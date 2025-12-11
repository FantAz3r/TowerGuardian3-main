using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnerActivator : MonoBehaviour
{
    public event Action<SpawnerActivator> Detected;
    public event Action<SpawnerActivator> Losted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))  
        {
            Detected?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Losted?.Invoke(this);
        }
    }
}
