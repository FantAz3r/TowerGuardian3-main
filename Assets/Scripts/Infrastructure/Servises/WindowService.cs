using UnityEngine;

public class WindowService : IWindowService
{
    private readonly UIFactory _uiFactory;

    public WindowService(UIFactory uiFactory) =>
        _uiFactory = uiFactory;

    public WindowBase Open(WindowType type, GameObject payload = null)
    {
        WindowBase window = null;

        switch (type)
        {
            case WindowType.None:
                break;

            case WindowType.QuestViewer:
                window = _uiFactory.CreateQuestViewer();
                break;

            case WindowType.Shop:
                window = _uiFactory.CreateShop();
                _uiFactory.CloseHUD();
                break;

            case WindowType.Sell:
                window = _uiFactory.CreateSell();
                _uiFactory.CloseHUD();
                break;

            case WindowType.WinLevelMenu:
                window = _uiFactory.CreateWinLevelMenu();
                _uiFactory.CloseHUD();
                break;

            case WindowType.StartLevelMenu:
                window = _uiFactory.CreateStartLevelMenu(payload);
                _uiFactory.CloseHUD();
                break;

            case WindowType.LouseLevelMenu:
                window = _uiFactory.CreateLouseLevelMenu(payload);
                _uiFactory.CloseHUD();
                break;

            case WindowType.Settings:
                window = _uiFactory.CreateSettings();
                _uiFactory.CloseHUD();
                break;

            case WindowType.Pause:
                window = _uiFactory.CreatePauseUI();
                _uiFactory.CloseHUD();
                break;

            case WindowType.CardMenu:
                window = _uiFactory.CreateCardSelectionMenu();
                _uiFactory.CloseHUD();
                break;

            case WindowType.HUD:
                window = _uiFactory.CreateHUD();
                break;

            case WindowType.MainMenu:
                window = _uiFactory.CreateMainMenu();
                break;

            case WindowType.MainSettings:
                window = _uiFactory.CreateSettings();
                break;

            case WindowType.Background:
                window = _uiFactory.CreateBackground();
                break;

            case WindowType.DamageScreen:
                window = _uiFactory.CreateDamageScreen();
                break;
        }

        if (window != null)
        {
            window.Open();
            Debug.Log(type);
        }

        return window;
    }

    public void CreateUIRoot()
    {
        _uiFactory.CreateUIRoot();
    }
}
