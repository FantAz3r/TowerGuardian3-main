using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int damagePerTick = 2;        
    public float damageInterval = 1f;
    private WaitForSeconds _delay;

    private Dictionary<Health, Coroutine> damagedObjects = new Dictionary<Health, Coroutine>();

    private void Awake()
    {
        _delay = new WaitForSeconds(damageInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health damageable = other.GetComponent<Health>();

        if (damageable != null && !damagedObjects.ContainsKey(damageable))
        {
            Coroutine damageCoroutine = StartCoroutine(DamageOverTime(damageable));
            damagedObjects.Add(damageable, damageCoroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Health damageable = other.GetComponent<Health>();

        if (damageable != null && damagedObjects.ContainsKey(damageable))
        {
            StopCoroutine(damagedObjects[damageable]);
            damagedObjects.Remove(damageable);
        }
    }

    private IEnumerator DamageOverTime(Health target)
    {
        while (enabled)
        {
            yield return _delay;
            target.TakeDamage(damagePerTick);
        }
    }
}
