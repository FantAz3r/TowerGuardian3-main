using System;
using UnityEngine;


public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    public event Action Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Entered?.Invoke();
        }
    }
}