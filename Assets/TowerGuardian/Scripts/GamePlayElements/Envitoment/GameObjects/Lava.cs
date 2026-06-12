using System.Collections.Generic;
using TowerGuardian.Scripts.GamePlayElements.Entity;
using UnityEngine;

namespace TowerGuardian.Scripts.GamePlayElements.Envitoment.GameObjects
{
    public class Lava : MonoBehaviour
    {
        [SerializeField]
        private int _damagePerTick = 2;
        [SerializeField]
        private float _damageInterval = 0.5f;
        private float _lastDamageTime;

        private List<Health> _entities = new ();

        private void OnTriggerEnter(Collider other)
        {
            Health damageable = other.GetComponent<Health>();

            if (damageable != null && !_entities.Contains(damageable))
            {
                _entities.Add(damageable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Health damageable = other.GetComponent<Health>();

            if (damageable != null && _entities.Contains(damageable))
            {
                _entities.Remove(damageable);
            }
        }

        private void Update()
        {
            if (_entities.Count == 0)
            {
                return;
            }

            if (Time.time >= _lastDamageTime + _damageInterval)
            {
                _lastDamageTime = Time.time;

                foreach (Health health in _entities)
                {
                    health.TakeDamage(_damagePerTick);
                }
            }
        }
    }
}
