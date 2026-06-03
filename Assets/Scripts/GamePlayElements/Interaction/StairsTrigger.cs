using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StairsTrigger : MonoBehaviour
{
    private Collider _collider;

    public event Action Entered;
    public Vector3 Center => _collider.bounds.center;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Entered?.Invoke();
        }
    }
}
