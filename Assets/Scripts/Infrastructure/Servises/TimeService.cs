using System.Collections;
using UnityEngine;

public class TimeService : IService, ITimeService
{
    private readonly ICoroutineRunner _coroutineRunner;
    private Coroutine _pauseCoroutine;
    private WaitForSeconds _wait;
    private float _time;

    public bool IsPaused { get; private set; }

    public TimeService(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
        _wait = new WaitForSeconds(_time);
    }

    public void Pause()
    {
        if (IsPaused)
            return;

        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        if (IsPaused == false)
            return;

        Time.timeScale = 1f;
        IsPaused = false;

        if (_pauseCoroutine != null)
        {
            _coroutineRunner.StopCoroutine(_pauseCoroutine);
            _pauseCoroutine = null;
        }
    }

    public void PauseForSeconds(float seconds)
    {
        if (_pauseCoroutine != null)
            _coroutineRunner.StopCoroutine(_pauseCoroutine);

        _time = seconds;
        _pauseCoroutine = _coroutineRunner.StartCoroutine(PauseCoroutine(seconds));
    }

    private IEnumerator PauseCoroutine(float seconds)
    {
        Pause();

        yield return _wait;

        Resume();
        _pauseCoroutine = null;
    }
}


