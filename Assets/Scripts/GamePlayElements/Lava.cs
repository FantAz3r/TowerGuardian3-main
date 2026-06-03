using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] private int damagePerTick = 2;
    [SerializeField] private float damageInterval = 0.5f;
    private float _lastDamageTime = 0;

    private List<Health> _entities = new ();

    private void OnTriggerEnter(Collider other)
    {
        Health damageable = other.GetComponent<Health>();

        if (damageable != null && _entities.Contains(damageable) == false)
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
        if (_entities.Count == 0) return;

        if (Time.time >= _lastDamageTime + damageInterval)
        {
            _lastDamageTime = Time.time;

            foreach (Health health in _entities)
            {
                health.TakeDamage(damagePerTick);
            }
        }
    }
}
