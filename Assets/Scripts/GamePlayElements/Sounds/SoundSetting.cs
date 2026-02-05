using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Slider))]
public class SoundSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private List<string> _volumeParameters;

    private float _minVolume = 0.0001f;
    private float _minDecibels = -80f;
    private float _maxDecibels = 0f;
    private Slider _volumeSlider;
    private float _currentValue;

    private void Awake()
    {
        LoadVolume();
        _volumeSlider = GetComponent<Slider>();

        if (_volumeParameters.Count > 0 && _audioMixer.GetFloat(_volumeParameters[0], out float dB))
        {
            if (_currentValue == 0)
            {
                _currentValue = dB;
            }

            _volumeSlider.value = Mathf.Pow(10, _currentValue / 20);
        }

        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void OnDestroy()
    {
        if (_volumeSlider != null)
            _volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        float dB;

        if (sliderValue > _minVolume)
            dB = 20f * Mathf.Log10(sliderValue);
        else
            dB = _minDecibels;

        dB = Mathf.Clamp(dB, _minDecibels, _maxDecibels);
        _currentValue = dB;
        SaveVolume();

        foreach (var param in _volumeParameters)
        {
            _audioMixer.SetFloat(param, dB);
        }
    }

    private void SaveVolume()
    {
        if (YG2.saves.Volumes == null)
            YG2.saves.Volumes = new List<SoundSaveData>();

        foreach (var volumeParam in _volumeParameters)
        {
            int index = YG2.saves.Volumes.FindIndex(v => v.Name == volumeParam);

            if (index >= 0)
            {
                YG2.saves.Volumes[index] = new SoundSaveData(volumeParam, _currentValue);
            }
            else
            {
                YG2.saves.Volumes.Add(new SoundSaveData(volumeParam, _currentValue));
            }
        }

        YG2.SaveProgress();
    }

    private void LoadVolume()
    {
        if (YG2.saves.Volumes == null || _volumeParameters.Count == 0)
            return;

        SoundSaveData soundData = YG2.saves.Volumes.Find(v => v.Name == _volumeParameters[0]);

        if (string.IsNullOrEmpty(soundData.Name) == false)
        {
            _currentValue = soundData.Volume;

            foreach (var param in _volumeParameters)
            {
                _audioMixer.SetFloat(param, _currentValue);
            }
        }
    }
}
