using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private RectTransform _clockImage;
    [SerializeField] private TMP_Text _remainingTime;
    [SerializeField] private Image _infiniteImage;

    private DayCycle _dayCycle;
    private DayPhase _currentPhase;
    private float _currentPhaseDuration;

    public void Init(DayCycle dayCycle)
    {
        _dayCycle = dayCycle;
        _infiniteImage.gameObject.SetActive(false);
        _dayCycle.OnPhaseChanged += OnPhaseChanged;
        _dayCycle.TimePassedFromTransition += OnTimePassedFromTransition;
        _dayCycle.OnPhaseInfinited += SetInfiniteTime;
    }

    private void OnDestroy()
    {
        if (_dayCycle == null) return;

        _dayCycle.OnPhaseChanged -= OnPhaseChanged;
        _dayCycle.TimePassedFromTransition -= OnTimePassedFromTransition;
        _dayCycle.OnPhaseInfinited -= SetInfiniteTime;
    }

    private void OnPhaseChanged(DayPhase phase)
    {
        _currentPhase = phase;

        if (phase == DayPhase.Day)
            _currentPhaseDuration = _dayCycle.DayDuration + _dayCycle.TransitionDuration;
        else
            _currentPhaseDuration = _dayCycle.NightDuration + _dayCycle.TransitionDuration;
    }

    private void OnTimePassedFromTransition(float timePassed)
    {
        if (_currentPhaseDuration <= 0)
            return;

        float oneMinute = 60f;
        float timeRemaining = _currentPhaseDuration - timePassed;
        float rotationSpeed = 180f / _currentPhaseDuration;
        float angle = timePassed * rotationSpeed;

        SetClockRotation(angle, _currentPhase);


        int minutes = Mathf.FloorToInt(timeRemaining / oneMinute);
        int seconds = Mathf.FloorToInt(timeRemaining % oneMinute);
        _remainingTime.text = $"{minutes:00}:{seconds:00}";
    }

    private void SetInfiniteTime(DayPhase phase)
    {
        _dayCycle.OnPhaseChanged -= OnPhaseChanged;
        _dayCycle.TimePassedFromTransition -= OnTimePassedFromTransition;
        float quarter = 90f;
        _infiniteImage.gameObject.SetActive(true);
        _remainingTime.gameObject.SetActive(false);
        SetClockRotation(quarter, phase);
    }

    private void SetClockRotation(float angle, DayPhase phase)
    {
        float half = 180f;

        if (phase == DayPhase.Night)
        {
            _clockImage.localEulerAngles = new Vector3(0, 0, angle);
        }
        else
        {
            _clockImage.localEulerAngles = new Vector3(0, 0, angle + half);
        }
    }
}
