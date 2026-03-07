using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthRegeneration : MonoBehaviour, IBuffble
{
    [SerializeField] private PlayerConfig _config;

    private Health _health;
    private float _delay = 2f;
    private float _regenAccumulated = 0f;
    private float _startRegenValue = 1;
    private float _regenValue;
    private bool _isRegeneration = false;
    private float _timer = 0f;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _startRegenValue = _config.HealthRegeneration;
        _regenValue = _startRegenValue;
    }

    public void EnableBuff()
    {
        _isRegeneration = true;
    }

    private void OnDestroy()
    {
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

    public void ApplyBuff(float value)
    {
        _isRegeneration = true;
        _regenValue = _startRegenValue * (1 + value);
    }

    public void RemoveBuff()
    {
        _isRegeneration = false;
    }
}
