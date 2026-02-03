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
    private float _timeSincePhaseChange = 0;
    private float _totalTimeOnLevel = 0;

    private Coroutine _phaseCoroutine;
    private Light _directionalLight;
    private Color _dayLightColor;
    private Color _nightLightColor;
    private DayPhase _currentPhase;

    public float DayDuration => _dayDuration;
    public float NightDuration => _nightDuration;
    public float TransitionDuration => _transitionDuration;

    public event Action<DayPhase> OnPhaseInfinited;
    public event Action<DayPhase> OnPhaseChanged;
    public event Action<float> TimePassedFromTransition;
    public event Action<float> TimePassedFromStart;

    public DayPhase CurrentPhase => _currentPhase;

    private DayPhase GetNextPhase(DayPhase current)
        => current == DayPhase.Day ? DayPhase.Night : DayPhase.Day;

    private float GetPhaseDuration(DayPhase phase)
        => phase == DayPhase.Day ? _dayDuration : _nightDuration;

    private Color GetColor(DayPhase phase)
        => phase == DayPhase.Day ? _dayLightColor : _nightLightColor;

    private float GetIntensity(DayPhase phase)
        => phase == DayPhase.Day ? _dayLightIntensity : _nightLightIntensity;


    private void Awake()
    {
        _currentPhase = DayPhase.Day;
    }

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

    public void StartDayCycle()
    {
        _phaseCoroutine = StartCoroutine(CheckForInfinite());
    }

    public void StopDayCycle()
    {
        if(_phaseCoroutine != null)
        {
            StopCoroutine(_phaseCoroutine);
        }
    }

    private IEnumerator CycleCoroutine()
    {
        _totalTimeOnLevel = 0f;

        while (enabled)
        {
            OnPhaseChanged?.Invoke(_currentPhase);
            _timeRemaining = GetPhaseDuration(_currentPhase) + _transitionDuration;
            _timeSincePhaseChange = 0f;

            while (_timeRemaining > _transitionDuration)
            {
                float deltaTime = Time.deltaTime;
                UpdateTimes(deltaTime);
                yield return null;
            }

            var nextPhase = GetNextPhase(_currentPhase);
            yield return StartCoroutine(TransitionLighting(_currentPhase, nextPhase));
            _currentPhase = nextPhase;
        }
    }

    private IEnumerator TransitionLighting(DayPhase fromPhase, DayPhase toPhase)
    {
        Color startColor = GetColor(fromPhase);
        Color endColor = GetColor(toPhase);
        float startIntensity = GetIntensity(fromPhase);
        float endIntensity = GetIntensity(toPhase);

        float time = 0f;

        while (time < _transitionDuration)
        {
            float deltaTime = Time.deltaTime;
            UpdateTimes(deltaTime);
            time += deltaTime;
            float t = time / _transitionDuration;

            TimePassedFromStart?.Invoke(_totalTimeOnLevel);
            TimePassedFromTransition?.Invoke(_timeSincePhaseChange);

            _directionalLight.color = Color.Lerp(startColor, endColor, t);
            _directionalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, t);
            yield return null;
        }

        ApplyLighting(endColor, endIntensity);
    }

    private void ApplyLighting(Color color, float intensity)
    {
        _directionalLight.color = color;
        _directionalLight.intensity = intensity;
    }

    private void UpdateTimes(float deltaTime)
    {
        _timeRemaining -= deltaTime;
        _timeSincePhaseChange += deltaTime;
        _totalTimeOnLevel += deltaTime;

        TimePassedFromStart?.Invoke(_totalTimeOnLevel);
        TimePassedFromTransition?.Invoke(_timeSincePhaseChange);
    }

    private IEnumerator CheckForInfinite()
    {
        yield return new WaitForSeconds(0.1f);

        if (_dayDuration == -1)
        {
            ApplyLighting(_dayLightColor, _dayLightIntensity);
            yield break;
        }

        if (_nightDuration == -1)
        {
            ApplyLighting(_nightLightColor, _nightLightIntensity);
            yield break;
        }

        ApplyLighting(_dayLightColor, _dayLightIntensity);
        _phaseCoroutine = StartCoroutine(CycleCoroutine());
    }
}
