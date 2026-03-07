using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public int damagePerTick = 2;        
    public float damageInterval = 0.5f;
    private WaitForSeconds _delay;

    private Dictionary<Health, Coroutine> _damagedObjects = new Dictionary<Health, Coroutine>();

    private void Awake()
    {
        _delay = new WaitForSeconds(damageInterval);
    }

    private void OnTriggerEnter(Collider other)
    {
        Health damageable = other.GetComponent<Health>();

        if (damageable != null && _damagedObjects.ContainsKey(damageable) == false)
        {
            Coroutine damageCoroutine = StartCoroutine(DamageOverTime(damageable));
            _damagedObjects.Add(damageable, damageCoroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Health damageable = other.GetComponent<Health>();

        if (damageable != null && _damagedObjects.ContainsKey(damageable))
        {
            StopCoroutine(_damagedObjects[damageable]);
            _damagedObjects.Remove(damageable);
        }
    }

    private IEnumerator DamageOverTime(Health target)
    {
        while (enabled)
        {
            target.TakeDamage(damagePerTick);
            yield return _delay;
        }
    }
}
