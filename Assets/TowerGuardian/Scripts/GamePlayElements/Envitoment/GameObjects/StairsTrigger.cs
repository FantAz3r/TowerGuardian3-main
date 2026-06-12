using System;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
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
}
