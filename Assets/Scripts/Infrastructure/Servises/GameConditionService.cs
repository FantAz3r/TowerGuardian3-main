using UnityEngine;

public class GameConditionService : IGameConditionService
{
    private IWindowService _windowService;
    public bool IsLevelEnded { get; private set; } = false;
    public bool IsEndLevelWindowOpen { get; private set; } = false;

    public GameConditionService(IWindowService windowService) => _windowService = windowService;

    public void OnLouse(GameObject louseReason = null)
    {
        IsEndLevelWindowOpen = true;
        _windowService.Open(WindowType.LouseLevelMenu, louseReason);
    }

    public void OnStart(Portal portal)
    {
        IsEndLevelWindowOpen = true;
        _windowService.Open(WindowType.StartLevelMenu, portal.gameObject);
    }

    public void OnWin()
    {
        IsEndLevelWindowOpen = true;
        _windowService.Open(WindowType.WinLevelMenu);
        SetLevelEnded(true);
    }

    public void SetLevelEnded(bool isLevelEnded = true)
    {
        IsLevelEnded = isLevelEnded;
    }

    public void SetEndLevelWindowOpen(bool isEndLevelWindowOpen)
    {
        IsEndLevelWindowOpen = isEndLevelWindowOpen;
    }
}
