using UnityEngine;

public class WindowService : IWindowService
{
    private readonly UIFactory _uiFactory;
    private WindowBase _currentWindow = null;
    private WindowType _currentWindowType = WindowType.None;
    private WindowType _previousWindowType = WindowType.None;
    public WindowService(UIFactory uiFactory) =>
        _uiFactory = uiFactory;

    public WindowBase Open(WindowType type, GameObject payload = null)
    {
        if (type == WindowType.None)
            return null;

        if (type != _currentWindowType)
        {
            _previousWindowType = _currentWindowType;
            _currentWindowType = type;
        }

        switch (type)
        {
            case WindowType.None:
                break;

            case WindowType.WaveViewer:
                _currentWindow = _uiFactory.CreateWaveViewer();
                break;
            case WindowType.QuestViewer:
                _currentWindow = _uiFactory.CreateQuestViewer();
                break;

            case WindowType.Shop:
                _currentWindow = _uiFactory.CreateShop();
                _uiFactory.CloseHUD();
                break;

            case WindowType.Sell:
                _currentWindow = _uiFactory.CreateSell();
                _uiFactory.CloseHUD();
                break;

            case WindowType.WinLevelMenu:
                _currentWindow = _uiFactory.CreateWinLevelMenu();
                _uiFactory.CloseHUD();
                break;

            case WindowType.StartLevelMenu:
                _currentWindow = _uiFactory.CreateStartLevelMenu(payload);
                _uiFactory.CloseHUD();
                break;

            case WindowType.LouseLevelMenu:
                _currentWindow = _uiFactory.CreateLouseLevelMenu(payload);
                _uiFactory.CloseHUD();
                break;

            case WindowType.Settings:
                _currentWindow = _uiFactory.CreateSettings();
                _uiFactory.CloseHUD();
                break;

            case WindowType.Pause:
                _currentWindow = _uiFactory.CreatePauseUI();
                _uiFactory.CloseHUD();
                break;

            case WindowType.CardMenu:
                _currentWindow = _uiFactory.CreateCardSelectionMenu();
                _uiFactory.CloseHUD();
                break;

            case WindowType.HUD:
                _currentWindow = _uiFactory.CreateHUD();
                break;

            case WindowType.MainMenu:
                _currentWindow = _uiFactory.CreateMainMenu();
                break;

            case WindowType.MainSettings:
                _currentWindow = _uiFactory.CreateSettings();
                break;

            case WindowType.Background:
                _currentWindow = _uiFactory.CreateBackground();
                break;

            case WindowType.DamageScreen:
                _currentWindow = _uiFactory.CreateDamageScreen();
                break;

            case WindowType.LeaderBoard:
                _currentWindow = _uiFactory.CreateLeaderboard();
                _uiFactory.CloseHUD();
                break;
            case WindowType.Inventory:
                _currentWindow = _uiFactory.CreateInventory();
                _uiFactory.CloseHUD();
                break;
            case WindowType.MenuLeaderboard:
                _currentWindow = _uiFactory.CreateMenuLeaderboard();
                break;
                case WindowType.BossHealth:
                _currentWindow = _uiFactory.CreateBossHealthView();
                break;
        }

        if (_currentWindow != null)
        {
            _currentWindow.Open();
        }

        return _currentWindow;
    }

    public WindowBase OpenPreviousWindow()
    {
        if (_previousWindowType != WindowType.None)
        {
            var temp = _currentWindowType;
            _currentWindowType = _previousWindowType;
            _previousWindowType = temp;

            var window = Open(_currentWindowType);

            if (_currentWindow != null)
            {
                _currentWindow.Open();
            }

            return window;
        }
        else
        {
            return null;
        }
    }

    public void CreateUIRoot()
    {
        _uiFactory.CreateUIRoot();
    }

    public void CreateJoystick()
    {
        _uiFactory.CreateJoystick();
    }
}
