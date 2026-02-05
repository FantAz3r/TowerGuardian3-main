using System;
using UnityEngine;

public class GameConditionService : IGameConditionService
{
    private IWindowService _windowService;

    public GameConditionService(IWindowService windowService) => _windowService = windowService;

    public event Action LevelComplited, LevelLoused, LevelStarted;

    public void OnLouse(GameObject louseReason = null)
    {
        _windowService.Open(WindowType.LouseLevelMenu, louseReason);
    }

    public void OnStart(Portal portal)
    {
        _windowService.Open(WindowType.StartLevelMenu, portal.gameObject);
    }

    public void OnWin()
    {
        _windowService.Open(WindowType.WinLevelMenu);
    }
}
