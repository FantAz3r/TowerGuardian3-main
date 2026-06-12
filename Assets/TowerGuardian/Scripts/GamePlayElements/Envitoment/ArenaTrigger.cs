using System;
using TowerGuardian.Scripts.GamePlayElements.PlayerScripts;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment
{
    public class ArenaTrigger : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider _boxCollider;

        public event Action Entered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out _))
            {
                Entered?.Invoke();
            }
        }
    }
}