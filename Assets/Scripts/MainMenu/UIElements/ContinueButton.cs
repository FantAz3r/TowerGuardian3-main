using UnityEngine;
using UnityEngine.UI;
using YG;

public class ContinueButton : MonoBehaviour
{
    private LevelID _levelToLoad;
    private Button _button;
    private IStateSwitchService _switchService;

    private void Awake()
    {
        _switchService = ServiceLocator.Get<IStateSwitchService>();
        _button = GetComponent<Button>();
        LoadLevel();
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
        _button.interactable = false;
        _switchService.Switch(_levelToLoad);
    }

    private void LoadLevel()
    {
        if (YG2.saves.CurrentLevel == LevelID.None || YG2.saves.CurrentLevel == LevelID.MainMenu)
        {
            _levelToLoad = LevelID.Tower;
            return;
        }

        _levelToLoad = YG2.saves.CurrentLevel;
    }
}
