using UnityEngine;
using UnityEngine.UI;
using YG;

public class SwichDamageNumbers : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    private ISpawnerService _spawnerService;

    private void Awake() 
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();

        _toggle.isOn = true;

        if (YG2.saves != null)
        {
            _toggle.isOn = YG2.saves.ShowDamageNumber;
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

    private void ApplyToggleState(bool isActive)
    {
        if (isActive)
        {
            _spawnerService.EnableSpawner(SpawnerType.Text);
        }
        else
        {
            _spawnerService.DisableSpawn(SpawnerType.Text);
        }
    }

    private void SaveSetting(bool isActive)
    {
        YG2.saves.ShowDamageNumber = isActive;
        YG2.SaveProgress();
    }
}

