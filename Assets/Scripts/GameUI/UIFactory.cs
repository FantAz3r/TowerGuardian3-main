using Crystal;
using UnityEngine;

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
        _hud.Init(_gameFactory.Player, _gameFactory.Cycle);
        CreateShowCardsButton();

        return _hud;
    }

    public void CloseHUD()
    {
        _hud?.Close();
    }

    public QuestViewer CreateQuestViewer()
    {
        QuestViewer questViewer = CreateWindow(WindowType.QuestViewer, _hud.transform) as QuestViewer;
        return questViewer;
    }

    public ShowCardsButton CreateShowCardsButton()
    {
        ShowCardsButton cardButton = CreateWindow(WindowType.ShowCardsButton, _hud.transform) as ShowCardsButton;
        cardButton.Init(_gameFactory.Player);
        return cardButton;
    }

    public Shop CreateShop()
    {
        Shop shop = CreateWindow(WindowType.Shop) as Shop;
        shop.Init(_gameFactory.Player);
        return shop;
    }

    public Sell CreateSell()
    {
        Sell sell = CreateWindow(WindowType.Sell) as Sell;
        sell.Init(_gameFactory.Player);
        return sell;
    }

    public PauseUI CreatePauseUI()
    {
        PauseUI pause = CreateWindow(WindowType.Pause) as PauseUI;
        return pause;
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
        _cardMenu.Init(_gameFactory.Player);
        return _cardMenu;
    }

    public WinLevelMenu CreateWinLevelMenu()
    {
        WinLevelMenu winPanel = CreateWindow(WindowType.WinLevelMenu) as WinLevelMenu;
        winPanel.Init(_gameFactory.ScoreCounter, _gameFactory.CurrentLevel);
        return winPanel;
    }

    public LouseLevelMenu CreateLouseLevelMenu()
    {
        LouseLevelMenu louseLevelMenu = CreateWindow(WindowType.LouseLevelMenu) as LouseLevelMenu;
        louseLevelMenu.Init(_gameFactory.ScoreCounter, _gameFactory.CurrentLevel, _gameFactory.Player);
        return louseLevelMenu;
    }

    public StartLevelMenu CreateStartLevelMenu(GameObject portalObject)
    {
        portalObject.TryGetComponent(out Portal portal);

        StartLevelMenu startLevelMenu = CreateWindow(WindowType.StartLevelMenu) as StartLevelMenu;
        startLevelMenu.Init(_gameFactory.ScoreCounter, _gameFactory.CurrentLevel, portal.NextLevel);
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

