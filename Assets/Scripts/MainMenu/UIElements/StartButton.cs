using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private LevelID _levelToLoad;

    private Button _button;
    private IStateSwitchService _switchService;

    public void Init(IStateSwitchService switchService)
    {
        _switchService = switchService;
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
        _switchService.Switch(_levelToLoad);
    }
}
