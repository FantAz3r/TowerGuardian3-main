using Crystal;
using UnityEngine;
using YG;

public class UIFactory : IUIFactory
{
    private WindowData _windowData;
    private IGameFactory _gameFactory;
    private Transform _uiRoot;
    private Transform _backgroundContainer;

    public UIFactory()
    {
        _gameFactory = ServiceLocator.Get<IGameFactory>();
        _windowData = Resources.Load<WindowData>(GameConstants.WindowData);
    }

    public HUD HUD { get; private set; }

    public void CreateUIRoot()
    {
        _backgroundContainer = Object.Instantiate(Resources.Load<RectTransform>(GameConstants.UIRoot));
        _uiRoot = _backgroundContainer.GetComponentInChildren<SafeArea>().transform;
    }

    public MainMenu CreateMainMenu()
    {
        Settings settings = CreateSettings();
        settings.Close();

        return CreateWindow(WindowType.MainMenu) as MainMenu;
    }

    public WindowBase CreateBackground()
    {
        WindowBase background = CreateWindow(WindowType.Background, _backgroundContainer);
        background.transform.SetAsFirstSibling();
        return background;
    }

    public HUD CreateHUD()
    {
        ServiceLocator.Get<IGameConditionService>().SetEndLevelWindowOpen(false);

        if (HUD != null)
        {
            HUD.Open();
            return HUD;
        }

        HUD = CreateWindow(WindowType.HUD) as HUD;
        CreateShowCardsButton();

        return HUD;
    }

    public void CloseHUD()
    {
        HUD?.Close();
    }

    public BossHealthViewer CreateBossHealthView()
    {
        return CreateWindow(WindowType.BossHealth, HUD.transform) as BossHealthViewer;
    }

    public WaveViewer CreateWaveViewer()
    {
        return CreateWindow(WindowType.WaveViewer, HUD.transform) as WaveViewer;
    }

    public DamageScreen CreateDamageScreen()
    {
        DamageScreen window = CreateWindow(WindowType.DamageScreen, _backgroundContainer) as DamageScreen;
        window.transform.SetAsFirstSibling();
        return window;
    }

    public QuestViewer CreateQuestViewer()
    {
        return CreateWindow(WindowType.QuestViewer, HUD.transform) as QuestViewer;
    }

    public void CreateJoystick()
    {
        if (YG2.envir.isDesktop == false)
        {
            IInputService inpusService = ServiceLocator.Get<IInputService>().GetSelf();
            Joystick joystick = Object.Instantiate(Resources.Load<Joystick>(GameConstants.Joystick), _backgroundContainer);
            joystick.transform.SetAsFirstSibling();

            if (inpusService is MobileInput mobileInput)
            {
                mobileInput.Init(joystick);
            }
        }
    }

    public CardInventory CreateInventory()
    {
        return CreateWindow(WindowType.Inventory) as CardInventory;
    }

    public PauseWindow CreateLeaderboard()
    {
        return CreateWindow(WindowType.LeaderBoard) as PauseWindow;
    }

    public PauseWindow CreateMenuLeaderboard()
    {
        return CreateWindow(WindowType.MenuLeaderboard) as PauseWindow;
    }

    public ShowCardsButton CreateShowCardsButton()
    {
        return CreateWindow(WindowType.ShowCardsButton, HUD.transform) as ShowCardsButton;
    }

    public Shop CreateShop()
    {
        return CreateWindow(WindowType.Shop) as Shop;
    }

    public Sell CreateSell()
    {
        return CreateWindow(WindowType.Sell) as Sell;
    }

    public PauseUI CreatePauseUI()
    {
        if (_gameFactory.LevelConfig == null)
            return null;

        if (((int)_gameFactory.LevelConfig.Level) >= 3)
        {
            PauseUI pause = CreateWindow(WindowType.Pause) as PauseUI;
            return pause;
        }

        return null;
    }

    public Settings CreateSettings()
    {
        Settings settings = null;

        if (_gameFactory.CurrentLevel == LevelID.MainMenu)
        {
            settings = CreateWindow(WindowType.MainSettings) as Settings;
        }
        else
        {
            settings = CreateWindow(WindowType.Settings) as Settings;
        }

        return settings;
    }

    public CardSelectionMenu CreateCardSelectionMenu()
    {
        return CreateWindow(WindowType.CardMenu) as CardSelectionMenu;
    }

    public WinLevelMenu CreateWinLevelMenu()
    {
        return CreateWindow(WindowType.WinLevelMenu) as WinLevelMenu;
    }

    public LouseLevelMenu CreateLouseLevelMenu(GameObject louseReasonObject)
    {
        LouseLevelMenu louseLevelMenu = CreateWindow(WindowType.LouseLevelMenu) as LouseLevelMenu;

        if (louseReasonObject != null && louseReasonObject.TryGetComponent(out Health louseReason))
        {
            louseLevelMenu.SetResurrection();
        }

        return louseLevelMenu;
    }

    public StartLevelMenu CreateStartLevelMenu(GameObject portalObject)
    {
        portalObject.TryGetComponent(out Portal portal);

        StartLevelMenu startLevelMenu = CreateWindow(WindowType.StartLevelMenu) as StartLevelMenu;
        startLevelMenu.Init(portal.NextLevel);
        return startLevelMenu;
    }

    private WindowBase CreateWindow(WindowType windowType, Transform parent = null)
    {
        WindowBase prefab = null;

        foreach (var info in _windowData.WindowInfos)
        {
            if (info.Type == windowType)
            {
                prefab = info.Pefab;
            }
        }

        WindowBase window;

        if (parent == null)
            window = Object.Instantiate(prefab, _uiRoot);
        else
            window = Object.Instantiate(prefab, parent);

        return window;
    }
}