using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class StairsTrigger : MonoBehaviour
{
    private Collider _collider;
    public Vector3 Center => _collider.bounds.center;
    public event Action Entered;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

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
