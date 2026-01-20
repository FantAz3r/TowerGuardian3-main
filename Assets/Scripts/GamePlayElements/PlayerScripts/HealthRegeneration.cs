using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthRegeneration : MonoBehaviour, IBuffble
{
    [SerializeField] private PlayerConfig _config;

    private Health _health;
    private WaitForSeconds _wait;
    private Coroutine _regenCoroutine;
    private float _delay = 2f;
    private float _regenAccumulated = 0f;
    private float _regenValue;
    private bool _canRegen = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _wait = new WaitForSeconds(_delay);
        _regenValue = _config.HealthRegeneration;
        _health.DamageTaken += StartRegeneration;
    }

    public void EnableBuff()
    {
        _canRegen = true;
    }

    private void OnDestroy()
    {
        _health.DamageTaken -= StartRegeneration;
        StopRegeneration();
    }

    private void StartRegeneration(float useles = 0)
    {
        if (_canRegen && _regenCoroutine == null)
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
        while (_health.CurrentHealth < _health.MaxHealth)
        {
            yield return _wait;
            _regenAccumulated += _regenValue * _delay;

            if (_regenAccumulated >= 1f)
            {
                int healAmount = (int)_regenAccumulated;
                _health.Heal(healAmount);
                _regenAccumulated -= healAmount;
            }
        }

        _regenCoroutine = null;
        _regenAccumulated = 0;
    }

    public void ApplyBuff(float value)
    {
        _regenValue = _regenValue * (1+ value);
    }
}
