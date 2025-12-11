using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class StartButton : MonoBehaviour
{
    private LevelID _levelToLoad;
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
        LoadLevel();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    public void OnClicked()
    {
        _switchService.Switch(_levelToLoad);
    }

    private void LoadLevel()
    {
        if (YG2.saves.CurrentLevel == null || string.IsNullOrEmpty(YG2.saves.CurrentLevel))
        {
            _levelToLoad = LevelID.Tower;
            return;
        }

        string levelName = YG2.saves.CurrentLevel; 
        LevelID levelToLoad;

        if (Enum.TryParse(levelName, out levelToLoad))
        {
            _levelToLoad = levelToLoad;
        }
    }
}
