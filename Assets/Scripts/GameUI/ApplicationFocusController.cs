using UnityEngine;

public class ApplicationFocusController : MonoBehaviour
{
    private ITimeService _timeService;

    private void Start()
    {
        _timeService = ServiceLocator.Get<ITimeService>();
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
        _timeService.PauseAll();
    }

    private void OnFocusReturn()
    {
        if (_timeService.IsPaused)
        {
            _timeService.Pause();
        }
        else
        {
            _timeService.Resume();
        }
    }
}
