using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _levelConfigs;

    private float _dayDuration;
    private float _nightDuration;
    private Light _directionalLight;
    private Color _dayLightColor ;
    private Color _nightLightColor;
    private float _dayLightIntensity;
    private float _nightLightIntensity;
    private float _transitionDuration;

    private DayPhase _currentPhase;
    private float _timeRemaining;

    private event Action<DayPhase> OnPhaseChanged;

    public void Init(int configNumber)
    {
        foreach (var levelConfig in _levelConfigs)
        {
            if (levelConfig.Level == configNumber)
            {
                _dayDuration = levelConfig.DayDuration;
                _nightDuration = levelConfig.NightDuration;
                _dayLightColor = levelConfig.DayLightColor;
                _nightLightColor = levelConfig.NightLightColor;
                _dayLightIntensity = levelConfig.DayLightIntensity;
                _nightLightIntensity = levelConfig.NightLightIntensity;
                _transitionDuration = levelConfig.TransitionDuration;
                _directionalLight = GetComponent<Light>();
                _timeRemaining = levelConfig.DayDuration;

            }
        }
    }

    private void Start()
    {
        _currentPhase = DayPhase.Day;
        UpdateLighting();
        StartCoroutine(CycleCoroutine());
    }

    private IEnumerator CycleCoroutine()
    {
        while (enabled)
        {
            OnPhaseChanged?.Invoke(_currentPhase);
            _timeRemaining = (_currentPhase == DayPhase.Day) ? _dayDuration : _nightDuration;

            while (_timeRemaining > 0f)
            {
                _timeRemaining -= Time.deltaTime;
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
