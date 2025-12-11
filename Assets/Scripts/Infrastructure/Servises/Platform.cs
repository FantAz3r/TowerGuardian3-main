using System;
using System.Collections;
using UnityEngine;

public class Platform : InteractionMethod
{
    [SerializeField] private float _interactionTime = 3f;

    private float _currentTime = 0f;
    private float _delta = 0.1f;
    private bool _playerInZone = false;

    private WaitForSeconds _wait;
    private Coroutine _timerCoroutine = null;

    public event Action PlayerEnteredZone;
    public event Action PlayerExitedZone;
    public event Action<float, float> TimerUpdated;
    public event Action Disabled;


    private void Awake()
    {
        _wait = new WaitForSeconds(_delta);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
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
        while (enabled)
        {
            if (_playerInZone)
            {
                _currentTime += _delta;

                if (_currentTime >= _interactionTime)
                {
                    _currentTime = _interactionTime;

                    Interact();
                    yield break;
                }
            }
            else
            {
                _currentTime -= _delta;
                if (_currentTime <= 0f)
                {
                    _currentTime = 0f;
                    yield break;
                }
            }

            TimerUpdated?.Invoke(_currentTime, _interactionTime);
            yield return _wait;
        }
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void DisableInteraction()
    {
        base.DisableInteraction();
        Disabled?.Invoke();
    }
}
