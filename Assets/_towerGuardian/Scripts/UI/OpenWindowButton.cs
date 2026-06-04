using TowerGuardian.Enums;
using TowerGuardian.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

public class OpenWindowButton : MonoBehaviour
{
    [SerializeField] private WindowBase _closeWindow;
    [SerializeField] private WindowType _openWindow;

    private IWindowService _windowService;
    private Button _button;

    private void Awake()
    {
        _windowService = ServiceLocator.Get<IWindowService>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _closeWindow.Close();

        if (_openWindow == WindowType.Previous)
        {
            _windowService.OpenPreviousWindow();
        }
        else
        {
            _windowService.Open(_openWindow);
        }
    }
}
