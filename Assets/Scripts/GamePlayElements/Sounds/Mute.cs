using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using YG;

public class Mute : MonoBehaviour
{
    private const string VolumeParameter = "MasterVolume";

    [SerializeField] private Toggle _toggle;
    [SerializeField] private AudioMixer _mixer;

    private void Awake()
    {
        _toggle.isOn = true;

        if (YG2.isFirstGameSession == false)
        {
            _toggle.isOn = YG2.saves.Mute;
        }

        _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        ApplyToggleState(_toggle.isOn);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        ApplyToggleState(isOn);
        SaveSetting(isOn);
    }

    private void ApplyToggleState(bool isMuted)
    {
        if (isMuted)
        {
            _mixer.SetFloat(VolumeParameter, 0);
        }
        else
        {
            _mixer.SetFloat(VolumeParameter, -80f);
        }
    }

    private void SaveSetting(bool isMuted)
    {
        YG2.saves.Mute = isMuted;
        YG2.SaveProgress();
    }
}

