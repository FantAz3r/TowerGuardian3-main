using YG;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    private ITimeService _timeService;

    public event Action<bool> OnPaused;

    public void Init(ITimeService timeService)
    {
        _timeService = timeService;
    }

    private void OnEnable()
    {
        YG2.onPauseGame += OnPause;
    }

    private void OnDisable()
    {
        YG2.onPauseGame -= OnPause;
    }

    private void OnPause(bool pause)
    {

        OnPaused?.Invoke(pause);

        if (pause)
        {
            _timeService.Pause();
        }
        else
        {
            _timeService.Resume();
        }
    }
}
