using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthRegeneration : MonoBehaviour, IBuffble
{
    [SerializeField] private PlayerConfig _config;

    private StatsCalculator _statsCalculator;
    private Health _health;

    private float _delay = 2f;
    private float _regenAccumulated = 0f;
    private float _startRegenValue = 1;
    private float _regenValue;
    private bool _isRegeneration = false;
    private float _timer = 0f;

    private void Awake()
    {
        _statsCalculator = new StatsCalculator();
        _health = GetComponent<Health>();

        _startRegenValue = _config.HealthRegeneration;
        _regenValue = _startRegenValue;

        _health.Died += DisableRegeneration;
        _health.Resurected += EnableBuff;
    }

    private void OnDestroy()
    {
        _health.Died -= DisableRegeneration;
        _health.Resurected -= EnableBuff;
        _isRegeneration = false;
    }

    private void Update()
    {
        if (_isRegeneration == false)
            return;

        if (_health.CurrentHealth >= _health.MaxHealth)
        {
            _timer = 0f;
            _regenAccumulated = 0f;
            return;
        }

        _timer += Time.deltaTime;

        if (_timer >= _delay)
        {
            _regenAccumulated += _regenValue * _delay;

            if (_regenAccumulated >= 1f)
            {
                int healAmount = (int)_regenAccumulated;
                _health.Heal(healAmount);
                _regenAccumulated -= healAmount;
            }

            _timer = 0f;
        }
    }

    public void EnableBuff()
    {
        _isRegeneration = true;
    }

    public void ApplyBuff(IEffect effect)
    {
        _isRegeneration = true;
        _statsCalculator.AddEffect(effect);
        _regenValue = _statsCalculator.Calculate(_startRegenValue);
    }

    public void Recalculate()
    {
        _regenValue = _statsCalculator.Calculate(_startRegenValue);
    }

    public void RemoveBuff(IEffect effect)
    {
        _statsCalculator.RemoveEffect(effect);
        _regenValue = _statsCalculator.Calculate(_startRegenValue);

        if(_statsCalculator.GetEffectsCount() == 0)
        {
            _isRegeneration = false;
        }
    }

    public void DisableRegeneration()
    {
        _isRegeneration = false;
    }
}
