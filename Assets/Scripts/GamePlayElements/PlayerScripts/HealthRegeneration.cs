using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthRegeneration : MonoBehaviour, IBuffble
{
    [SerializeField] private PlayerConfig _config;

    private Health _health;
    private float _delay = 0.1f;
    private WaitForSeconds _wait;

    private Coroutine _regenCoroutine;
    private float _regenValue;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _wait = new WaitForSeconds(_delay);
        _regenValue = _config.HealthRegeneration;
    }

    public void EnableBuff()
    {
        StartRegeneration();
    }

    private void OnDestroy()
    {
        StopRegeneration();
    }

    private void StartRegeneration()
    {
        if (_regenCoroutine == null && enabled && _health != null)
        {
            _regenCoroutine = StartCoroutine(RegenerationRoutine());
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

    private IEnumerator RegenerationRoutine()
    {
        while (enabled)
        {
            _health.Heal(_regenValue * _delay);
            yield return _wait;
        }
    }

    public void ApplyBuff(float value)
    {
        _regenValue += value;
    }
}
