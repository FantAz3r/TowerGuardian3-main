using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.PlayerScripts
{
    public class AttackZone : MonoBehaviour
    {
        [SerializeField] private LayerMask _attackableLayers;
        [SerializeField] private Color _gizmoColor = new Color(1f, 0f, 0f, 0.25f);
        private float _range;
        private IDemageable _selfHealth;

        private void Awake()
        {
            _selfHealth = GetComponentInParent<IDemageable>();
        }

        public List<Health> GetTargets(float range)
        {
            SetAttackData(range);
            List<Health> targets = new List<Health>();

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, _attackableLayers);

            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.GetComponent<IDemageable>() == _selfHealth)
                    continue;

                Health damageable = collider.gameObject.GetComponent<Health>();

                if (damageable == null)
                    continue;

                targets.Add(damageable);
            }

            return targets;
        }

        public void SetAttackData(float range)
        {
            _range = range;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(transform.position, _range);
        }
    }
}
