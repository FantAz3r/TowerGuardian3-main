using System;
using System.Collections;
using UnityEngine;

public class Platform : InteractionMethod
{
    [SerializeField] private float _interactionTime = 1.5f;
    [field: SerializeField] public WindowType WindowType { get; private set; }

    private float _currentTime = 0f;
    private bool _playerInZone = false;

    private IWindowService _windowService;
    private Player _player;
    private Coroutine _timerCoroutine = null;

    public event Action PlayerEnteredZone;
    public event Action PlayerExitedZone;
    public event Action<float, float> TimerUpdated;
    public event Action Disabled;

    private void Awake()
    {
        _windowService = ServiceLocator.Get<IWindowService>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerInZone = true;
            PlayerEnteredZone?.Invoke();
            _player = player;
            StartTimerCoroutine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _player = null;
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
                _currentTime += Time.deltaTime;

                if (_currentTime >= _interactionTime)
                {
                    _currentTime = _interactionTime;

                    Interact();
                    yield break;
                }
            }
            else
            {
                _currentTime -= Time.deltaTime;
                if (_currentTime <= 0f)
                {
                    _currentTime = 0f;
                    yield break;
                }
            }

            TimerUpdated?.Invoke(_currentTime, _interactionTime);
            yield return null;
        }
    }

    public override void Interact()
    {
        _windowService.Open(WindowType);
    }
}

