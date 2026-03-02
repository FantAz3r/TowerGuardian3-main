using Crystal;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using YG;

public class UIFactory
{
    private WindowData _windowData;
    private IGameFactory _gameFactory;
    private Transform _uiRoot;
    private Transform _backgroundContainer;
    private CardSelectionMenu _cardMenu;
    private HUD _hud;

    public UIFactory()
    {
        _gameFactory = ServiceLocator.Get<IGameFactory>();
        _windowData = Resources.Load<WindowData>(GameConstants.WindowData);
    }

    public void CreateUIRoot()
    {
        _backgroundContainer = Object.Instantiate(Resources.Load<RectTransform>(GameConstants.UIRoot));
        _uiRoot = _backgroundContainer.GetComponentInChildren<SafeArea>().transform;
    }

    public MainMenu CreateMainMenu()
    {
        Settings settings = CreateSettings();
        settings.Close();
        MainMenu mainMenu = CreateWindow(WindowType.MainMenu) as MainMenu;
        return mainMenu;
    }

    public WindowBase CreateBackground()
    {
        WindowBase background = CreateWindow(WindowType.Background, _backgroundContainer);
        background.transform.SetAsFirstSibling();
        return background;
    }

    public HUD CreateHUD()
    {
        if (_hud != null)
        {
            _hud.Open();
            return _hud;
        }

        _hud = CreateWindow(WindowType.HUD) as HUD;
        CreateShowCardsButton();

        return _hud;
    }

    public void CloseHUD()
    {
        _hud?.Close();
    }

    public WaveViewer CreateWaveViewer()
    {
        WaveViewer waveViewer = CreateWindow(WindowType.WaveViewer, _hud.transform) as WaveViewer;
        return waveViewer;
    }

    public DamageScreen CreateDamageScreen()
    {
        DamageScreen window = CreateWindow(WindowType.DamageScreen, _backgroundContainer) as DamageScreen;
        window.transform.SetAsFirstSibling();
        return window;
    }

    public QuestViewer CreateQuestViewer()
    {
        QuestViewer questViewer = CreateWindow(WindowType.QuestViewer, _hud.transform) as QuestViewer;
        return questViewer;
    }

    public void CreateJoystick()
    {
        if(YG2.envir.isDesktop == false)
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
        CardInventory inventory = CreateWindow(WindowType.Inventory) as CardInventory;
        return inventory;
    }

    public PauseWindow CreateLeaderboard()
    {
        PauseWindow leaderboard = CreateWindow(WindowType.LeaderBoard) as PauseWindow;
        return leaderboard;
    }

   public ShowCardsButton CreateShowCardsButton()
   {
       ShowCardsButton cardButton = CreateWindow(WindowType.ShowCardsButton, _hud.transform) as ShowCardsButton;
       return cardButton;
   }

    public Shop CreateShop()
    {
        Shop shop = CreateWindow(WindowType.Shop) as Shop;
        return shop;
    }

    public Sell CreateSell()
    {
        Sell sell = CreateWindow(WindowType.Sell) as Sell;
        return sell;
    }

    public PauseUI CreatePauseUI()
    {
        if(_gameFactory.LevelConfig == null)
            return null;

        if(((int)_gameFactory.LevelConfig.Level) >=3)
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
        _cardMenu = CreateWindow(WindowType.CardMenu) as CardSelectionMenu;
        return _cardMenu;
    }

    public WinLevelMenu CreateWinLevelMenu()
    {
        WinLevelMenu winPanel = CreateWindow(WindowType.WinLevelMenu) as WinLevelMenu;
        return winPanel;
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

