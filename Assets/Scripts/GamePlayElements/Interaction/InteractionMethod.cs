using System;
using System.Collections;
using UnityEngine;

public abstract class InteractionMethod : MonoBehaviour
{
    [field: SerializeField] public float InteractionTime { get; private set; } = 1.5f;
    [SerializeField] private Collider _collider;
    [SerializeField] private bool _canUpdate = false;

    private float _currentTime = 0f;
    private bool _playerInZone = false;
    private bool _isTimerUpdate = false;

    private Coroutine _timerCoroutine = null;

    public event Action PlayerEnteredZone;
    public event Action PlayerExitedZone;
    public event Action<float, float> TimerUpdated;
    public event Action Disabled;

    protected virtual void Awake()
    {
        _collider.isTrigger = true;
    }

    public abstract void Interact();

    public void Enable()
    {
        enabled = true;
        _collider.enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isTimerUpdate = true;
            _playerInZone = true;
            PlayerEnteredZone?.Invoke();
            StartTimerCoroutine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _playerInZone = false;
            PlayerExitedZone?.Invoke();

            StartTimerCoroutine();
        }
    }

    private void StartTimerCoroutine()
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = StartCoroutine(TimerRoutine());
    }

    private IEnumerator TimerRoutine()
    {
        while (_isTimerUpdate)
        {
            if (_playerInZone)
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= InteractionTime)
                {
                    _currentTime = InteractionTime;

                    Interact();
                    _isTimerUpdate = _canUpdate;
                    yield break;
                }
            }
            else
            {
                _currentTime -= Time.deltaTime;

                if (_currentTime <= 0f)
                {
                    _currentTime = 0f;
                    _isTimerUpdate = false;
                    yield break;
                }
            }

            TimerUpdated?.Invoke(_currentTime, InteractionTime);
            yield return null;
        }
    }
}
