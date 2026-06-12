using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

namespace TowerGuardian.Scripts.GamePlayElements.Sounds
{
    public class Mute : MonoBehaviour
    {
        private const string VolumeParameter = "MasterVolume";

        [SerializeField]
        private Toggle _toggle;
        [SerializeField]
        private AudioMixer _mixer;

        private void Awake()
        {
            bool isMuted = false;

            if (!YG2.isFirstGameSession && YG2.saves != null)
            {
                isMuted = YG2.saves.Mute;
            }

            _toggle.isOn = !isMuted;
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
            ApplyToggleState(!_toggle.isOn);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            bool isMuted = !isOn;
            ApplyToggleState(isMuted);
            SaveSetting(isMuted);
        }

        private void ApplyToggleState(bool isMuted)
        {
            if (isMuted)
            {
                _mixer.SetFloat(VolumeParameter, -80f);
            }
            else
            {
                _mixer.SetFloat(VolumeParameter, 0);
            }
        }

        private void SaveSetting(bool isMuted)
        {
            YG2.saves.Mute = isMuted;
            YG2.SaveProgress();
        }
    }
}