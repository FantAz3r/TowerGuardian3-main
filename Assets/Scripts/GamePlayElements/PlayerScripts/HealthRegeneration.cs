using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthRegeneration : MonoBehaviour
{
    [SerializeField] private PlayerConfig _config;

    private Health _health;
    private float _delay = 0.1f;
    private WaitForSeconds _wait;

    private Coroutine _regenCoroutine;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _wait = new WaitForSeconds(_delay);
        StartRegeneration();
    }

    private void OnDestroy()
    {
        StopRegeneration();
    }

    private void StartRegeneration()
    {
        if (_regenCoroutine == null)
        {
            _regenCoroutine = StartCoroutine(HealPerTime());
        }
    }

    private void StopRegeneration()
    {
        if (_regenCoroutine != null)
        {
            StopCoroutine(_regenCoroutine);
            _regenCoroutine = null;
        }
    }

    private IEnumerator HealPerTime()
    {
        _health.Heal(_config.HealthRegeneration * _delay);
        yield return _wait;
    }
}
