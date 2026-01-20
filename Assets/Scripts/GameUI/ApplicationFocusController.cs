using UnityEngine;

public class ApplicationFocusController : MonoBehaviour
{
    private ITimeService _timeService;

    private void Awake()
    {
        _timeService = ServicesLocator.GetService<ITimeService>();
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            OnFocusReturn();
        }
        else
        {
            OnFocusLost();
        }
    }

    private void OnFocusLost()
    {
        _timeService.Pause();
    }

    private void OnFocusReturn()
    {
        if (_timeService.IsPaused)
        {
            _timeService.PauseGame();
        }
        else
        {
            _timeService.Resume();
        }
    }
}
