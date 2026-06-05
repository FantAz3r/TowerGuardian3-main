using System.Collections.Generic;
using TowerGuardian.Scripts.StaticData.Structs.SaveData;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.Sounds
{
    [RequireComponent(typeof(Slider))]
    public class SoundSetting : MonoBehaviour
    {
        private const float DefaultSliderValue = 0.5f;
        private const int VolumeForce = 10;
        private const float VolumeCurrentValueMultiplier = 0.05f;

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private List<string> _volumeParameters;

        private float _minVolume = 0.0001f;
        private float _minDecibels = -80f;
        private float _maxDecibels = 0f;
        private Slider _volumeSlider;
        private float _currentValue;
        private float _lastSavedSliderValue;

        private void Awake()
        {
            _volumeSlider = GetComponent<Slider>();

            if (YG2.isFirstGameSession || YG2.saves == null || YG2.saves.Volumes == null)
            {
                _volumeSlider.value = DefaultSliderValue;
                SetVolume(_volumeSlider.value);
            }
            else
            {
                LoadVolume();
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

            foreach (var param in _volumeParameters)
            {
                _audioMixer.SetFloat(param, dB);
            }

            _lastSavedSliderValue = sliderValue;
            SaveVolume();
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
                    YG2.saves.Volumes[index] = new SoundSaveData(volumeParam, _lastSavedSliderValue);
                }
                else
                {
                    YG2.saves.Volumes.Add(new SoundSaveData(volumeParam, _lastSavedSliderValue));
                }
            }

            YG2.SaveProgress();
        }

        private void LoadVolume()
        {
            if (YG2.saves.Volumes == null || _volumeParameters.Count == 0)
                return;

            SoundSaveData soundData = YG2.saves.Volumes.Find(v => v.Name == _volumeParameters[0]);

            if (!string.IsNullOrEmpty(soundData.Name))
            {
                float loadedSliderValue = soundData.Volume;
                _volumeSlider.value = loadedSliderValue;
                _lastSavedSliderValue = loadedSliderValue;
                SetVolume(_volumeSlider.value);
            }
            else
            {
                _volumeSlider.value = DefaultSliderValue;
                SetVolume(_volumeSlider.value);
            }
        }
    }
}
