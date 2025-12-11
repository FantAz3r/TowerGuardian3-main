using System;
using System.Collections;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private LevelData _levelData;

    private float _dayDuration;
    private float _nightDuration;
    private float _dayLightIntensity;
    private float _nightLightIntensity;
    private float _transitionDuration;
    private float _timeRemaining;

    private Light _directionalLight;
    private Color _dayLightColor;
    private Color _nightLightColor;
    private DayPhase _currentPhase;

    public event Action<DayPhase> OnPhaseChanged;
    public event Action<float> TimePassedFromTransition;
    public event Action<float> TimePassedFromStart;
    
    public DayPhase CurrentPhase => _currentPhase;

    public void Init(LevelConfig config)
    {
        _dayDuration = config.DayDuration;
        _nightDuration = config.NightDuration;
        _dayLightColor = config.DayLightColor;
        _nightLightColor = config.NightLightColor;
        _dayLightIntensity = config.DayLightIntensity;
        _nightLightIntensity = config.NightLightIntensity;
        _transitionDuration = config.TransitionDuration;
        _timeRemaining = config.DayDuration;
        _directionalLight = GetComponent<Light>();
    }

    private void Start()
    {
        _currentPhase = DayPhase.Day;
        UpdateLighting();
        StartCoroutine(CycleCoroutine());
    }

    private IEnumerator CycleCoroutine()
    {
        float timeSincePhaseChange = 0f;
        float totalTimeOnLevel = 0f;

        while (enabled)
        {
            OnPhaseChanged?.Invoke(_currentPhase);
            _timeRemaining = (_currentPhase == DayPhase.Day) ? _dayDuration : _nightDuration;

            timeSincePhaseChange = 0f; 

            while (_timeRemaining > 0f)
            {
                float deltaTime = Time.deltaTime;
                _timeRemaining -= deltaTime;
                timeSincePhaseChange += deltaTime;
                totalTimeOnLevel += deltaTime;

                if (timeSincePhaseChange >= 1f)
                {
                    TimePassedFromTransition?.Invoke(timeSincePhaseChange);
                    TimePassedFromStart?.Invoke(totalTimeOnLevel);
                    timeSincePhaseChange = 0f;
                }

                yield return null;
            }

            DayPhase nextPhase = _currentPhase == DayPhase.Day ? DayPhase.Night : DayPhase.Day;
            yield return StartCoroutine(TransitionLighting(nextPhase));

            _currentPhase = nextPhase;
        }
    }

    private IEnumerator TransitionLighting(DayPhase nextPhase)
    {
        Color startColor = _currentPhase == DayPhase.Day ? _dayLightColor : _nightLightColor;
        Color endColor = nextPhase == DayPhase.Day ? _dayLightColor : _nightLightColor;
        float startIntensity = _currentPhase == DayPhase.Day ? _dayLightIntensity : _nightLightIntensity;
        float endIntensity = nextPhase == DayPhase.Day ? _dayLightIntensity : _nightLightIntensity;

        float time = 0f;

        while (time < _transitionDuration)
        {
            time += Time.deltaTime;
            float transitionTime = time / _transitionDuration;

            _directionalLight.color = Color.Lerp(startColor, endColor, transitionTime);
            _directionalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, transitionTime);
            yield return null;
        }

        _directionalLight.color = endColor;
        _directionalLight.intensity = endIntensity;
    }

    private void UpdateLighting()
    {
        if (_directionalLight != null)
        {
            if (_currentPhase == DayPhase.Day)
            {
                _directionalLight.color = _dayLightColor;
                _directionalLight.intensity = _dayLightIntensity;
            }
            else
            {
                _directionalLight.color = _nightLightColor;
                _directionalLight.intensity = _nightLightIntensity;
            }
        }
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0f, _timeRemaining);
    }
}
