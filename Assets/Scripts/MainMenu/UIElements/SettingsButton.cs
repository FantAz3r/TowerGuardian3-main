using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private Transform _uiRoot;
    [SerializeField] private Transform _settingsPanel;
    private Button _button;

    public void Init(Transform uiRoot, Transform settingsPanel)
    {
        _uiRoot = uiRoot;
        _settingsPanel = settingsPanel;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    public void OnClicked()
    {
        _uiRoot.gameObject.SetActive(false);
        _settingsPanel.gameObject.SetActive(true);
    }
}