using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Entity.Enemies
{
    public class ForceField : MonoBehaviour
    {
        [field: SerializeField] public Health Health { get; private set; }

        private void Awake()
        {
            Health.enabled = false;
        }
    }
}