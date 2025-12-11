using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameFactory
{
    private Transform _uiRoot;
    private LevelConfig _levelConfig;

    private Player _player;
    private PlayerExperience _experience;
    private Inventory _inventory;
    private PlayerAttacker _attacker;
    private Health _health;
    private AttackZone _attackZone;
    private PlayerMover _mover;
    private PlayerCardConfigContainer _cardHolder;
    private EnemyDetector _detector;

    private WeaponFactory _weaponFactory;
    private CardData _cardData;
    private AllAbilities _allAbilities;

    private Tutorial _tutorial;
    private QuestBuilder _questBuilder;

    private List<CardButton> _buttons = new ();
    private Shop _shop;
    private Sell _sell;
    private WinLevelMenu _winMenu;
    private LouseLevelMenu _loseMenu;
    private StartLevelMenu _startMenu;
    private ScoreCounter _scoreCounter;

    private PortalFactory _portalFactory;
    private List<Portal> _levelExits = new();
    private GameStateMachine _stateMachine;
    private DayCycle _cycle;
    private TowerRenderer _tower;
    private List<Floor> _floors = new();

    private OpenShopAction _openShopAction;
    private OpenSellAction _openSellAction;

    private IInputService _inputService;
    private ITimeService _timeService;
    private ISpawnerService _spawnerService;

    public GameFactory(IInputService inputService, ITimeService timeService, ISpawnerService spawnerService, GameStateMachine stateMachine)
    {
        _inputService = inputService;
        _timeService = timeService;
        _spawnerService = spawnerService;
        _stateMachine = stateMachine;
    }

    public void InitLevelObjects()
    {
        var scene = SceneManager.GetActiveScene();
        List<GameObject> parentObjects = scene.GetRootGameObjects().ToList();

        foreach(var parent in parentObjects)
        {
            List<SpawnbleEntity> resourceItems = parent.GetComponentsInChildren<SpawnbleEntity>().ToList();

            foreach(var item in resourceItems)
            {
                item.Init(_spawnerService);
            }
        }
    }

    public void SetLevelConfig(LevelID level)
    {
        LevelData levelData = Resources.Load<LevelData>(GameConstants.LevelData);

        foreach (var levelInfo in levelData.LevelInfos)
        {
            if (levelInfo.LevelID == level)
            {
                _levelConfig = levelInfo.Config;
            }
        }
    }

    public void CreatePlayer()
    {
        Player prefab = Resources.Load<Player>(GameConstants.Player);
        _player = Object.Instantiate(prefab, _levelConfig.PlayerSpawnPoint, Quaternion.identity);
        _player.GetComponentInChildren<PlayerMover>().Init(_inputService);
        _attacker = _player.GetComponentInChildren<PlayerAttacker>();
        _inventory = _player.GetComponentInChildren<Inventory>();
        _attackZone = _player.GetComponentInChildren<AttackZone>();
        _experience = _player.GetComponentInChildren<PlayerExperience>();
        _cardHolder = _player.GetComponentInChildren<PlayerCardConfigContainer>();
        _allAbilities = _player.GetComponentInChildren<AllAbilities>();
        _detector = _player.GetComponentInChildren<EnemyDetector>();
        _health = _player.GetComponent<Health>();
        _mover = _player.GetComponentInChildren<PlayerMover>();
    }

    public void CreateSpawners(ISpawnerService spawnerService)
    {
        DamageText prefab = Resources.Load<DamageText>(GameConstants.DamageText);

        ResourceData resourceData = Resources.Load<ResourceData>(GameConstants.ResourceData);
        EffectData effectData = Resources.Load<EffectData>(GameConstants.EffectData);

        PieceSpawner pieceSpawner = new PieceSpawner(resourceData);
        EffectSpawner effectSpawner = new EffectSpawner(effectData);

        spawnerService.RegisterSpawner(pieceSpawner);
        spawnerService.RegisterSpawner(new DamageNumberSpawner(prefab));
        spawnerService.RegisterSpawner(effectSpawner);
    }

    public void CreateWeaponFactory()
    {
        _weaponFactory = new WeaponFactory(_player.transform, _attackZone);
    }

    public void CreateCamera()
    {
        CameraFollower prefab = Resources.Load<CameraFollower>(GameConstants.MainCamera);
        CameraFollower camera = Object.Instantiate(prefab);
        camera.Init(_player.transform);
    }

    public void CreateEventSystem()
    {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    public void CreateUI()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.GameCanvas);
        _uiRoot = Object.Instantiate(prefab).transform;
    }

    public void CreateScoreCounter()
    {
        _scoreCounter = new ScoreCounter(_player, _levelConfig, _cycle);
    }

    public void CreatePauseUI()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.PauseUI);
        GameObject panel = Object.Instantiate(prefab, _uiRoot);
        PauseUI pauseUI = panel.GetComponentInChildren<PauseUI>();
        pauseUI.Init(_stateMachine);
    }

    public void CreateResourceView()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.ResourceViewPanel);
        Transform container = _uiRoot.GetComponentInChildren<UIDummy>().transform;
        GameObject panel = Object.Instantiate(prefab, container);
        panel.GetComponent<ResourceViewer>().Init(_inventory);
    }

    public void CreateCards()
    {
        _cardData = Resources.Load<CardData>(GameConstants.CardData);
    }

    public void CreateCardsSelectionMenu()
    {
        CardSelectionMenu prefab = Resources.Load<CardSelectionMenu>(GameConstants.CardSelectionMenu);
        CardData cardData = Resources.Load<CardData>(GameConstants.CardData);
        Transform container = _uiRoot.transform;
        CardSelectionMenu panel = Object.Instantiate(prefab, container);
        panel.Init( _experience, new CardSelector(cardData), _buttons);
    }

    public void CreateCardButtons()
    {
        int cardsCount = 3;
        CardButton prefab = Resources.Load<CardButton>(GameConstants.CardViewer);
        Transform container = _uiRoot.transform;

        for (int i = 0; i < cardsCount; i++)
        {
            CardButton button = Object.Instantiate(prefab, container);
            button.Init(_cardHolder, new List<ICardFactory> { _weaponFactory });
            _buttons.Add(button);
        }
    }

    public void CreateLight()
    {
        DayCycle prefab = Resources.Load<DayCycle>(GameConstants.DirectionLight);
        _cycle = Object.Instantiate(prefab);
        _cycle.Init(_levelConfig);
    }

    public void CreateEnemies()
    {
        EnemySpawner prefab = Resources.Load<EnemySpawner>(GameConstants.EnemySpawner);
        EnemySpawner spawner = Object.Instantiate(prefab);
        spawner.Init(_player.transform, _cycle, _levelConfig, _spawnerService);
    }

    public void CreatePlayerPanel()
    {
        GameObject prefab = Resources.Load<GameObject>(GameConstants.PlayerPanel);
        GameObject playerPanel = Object.Instantiate(prefab, _uiRoot.transform);

        AbilityPanel abilityPanel = playerPanel.GetComponentInChildren<AbilityPanel>();
        PlayerHealthViewer healthViewer = playerPanel.GetComponentInChildren<PlayerHealthViewer>();
        LevelViewer levelViewer = playerPanel.GetComponentInChildren<LevelViewer>();

        levelViewer.Init(_experience);
        healthViewer.Init(_health);
        abilityPanel.Init(_allAbilities);
    }

    public void CreateWeaponPanel()
    {
        WeaponPanel prefab = Resources.Load<WeaponPanel>(GameConstants.WeaponPanel);
        WeaponPanel panel = Object.Instantiate(prefab, _uiRoot.transform);
        panel.Init(_cardHolder, _weaponFactory, _attacker);
    }

    public void CreateActions()
    {
        List<IAction> actions = new List<IAction>();

        actions.Add(_openShopAction);
        actions.Add(_openSellAction);
        actions.Add(_uiRoot.GetComponentInChildren<OpenBuildMenuAction>());

        InteractionObjectFactory interactionObjectFactory = new InteractionObjectFactory(actions);
    }

    public void CreatePortalsFactory()
    {
        _portalFactory = new PortalFactory(_winMenu, _loseMenu, _startMenu);
        _levelExits = _portalFactory.Create(_levelConfig.PortalData, _floors);
    }

    public void CreatePlatform()
    {
        Platform prefab = Resources.Load<Platform>(GameConstants.Platform);

        Platform shopPlatform = Object.Instantiate(prefab, new Vector3(3.755f, -11.38f, -28.4f), Quaternion.identity);
        shopPlatform.Init(_openShopAction);

        Platform sellPlatform = Object.Instantiate(prefab, new Vector3(14.465f, -11.38f, -22.11f), Quaternion.identity);
        sellPlatform.Init(_openSellAction);
    }

    public void CreateShop()
    {
        Shop prefab = Resources.Load<Shop>(GameConstants.Shop);
        _shop = Object.Instantiate(prefab, _uiRoot.transform);
        _openShopAction = _uiRoot.GetComponentInChildren<OpenShopAction>(true);
        _shop.Init(_inventory);
    }

    public void CreateSellStation()
    {
        Sell prefab = Resources.Load<Sell>(GameConstants.Sell);
        _sell = Object.Instantiate(prefab, _uiRoot.transform);
        _openSellAction = _uiRoot.GetComponentInChildren<OpenSellAction>(true);
        _sell.Init(_inventory, _cardData, _cardHolder);
    }

    public void CreateEndLevelMenu()
    {
        WinLevelMenu winMenuPrefab = Resources.Load<WinLevelMenu>(GameConstants.WinMenu);
        _winMenu = Object.Instantiate(winMenuPrefab, _uiRoot.transform);

        ScoreViewer viewer = _winMenu.GetComponent<ScoreViewer>();
        viewer.Init(_scoreCounter);
        _winMenu.Init(_stateMachine, _scoreCounter, _levelConfig.Level);

        LouseLevelMenu louseMenuPrefab = Resources.Load<LouseLevelMenu>(GameConstants.LouseMenu);
        _loseMenu = Object.Instantiate(louseMenuPrefab, _uiRoot.transform);
        _loseMenu.Init(_stateMachine, _scoreCounter, _levelConfig.Level);
        _loseMenu.SetPlayerHealth(_health);
    }

    public void CreateStartLevelMenu()
    {
        StartLevelMenu prefab = Resources.Load<StartLevelMenu>(GameConstants.StartMenu);
        _startMenu = Object.Instantiate(prefab, _uiRoot.transform);

        ScoreViewer viewer = _startMenu.GetComponent<ScoreViewer>();
        _startMenu.Init(_stateMachine, _scoreCounter, _levelConfig.Level);
        viewer.Init(_scoreCounter);
    }

    public void CreateQuests()
    {
        _questBuilder = new QuestBuilder(_mover, _attacker, _inventory, _cardHolder, _detector, _levelExits);
    }

    public void CreateTutorial()
    {
        QuestData questData = Resources.Load<QuestData>(GameConstants.QuestData);
        Tutorial tutorialPrefab = Resources.Load<Tutorial>(GameConstants.Tutorial);

        _tutorial = Object.Instantiate(tutorialPrefab);
        _tutorial.Init(_questBuilder, questData, _levelConfig.Quests);
        _portalFactory.SetQuests(_tutorial);
    }

    public void CreateQuestViewer()
    {
        QuestViewer viewerPrefab = Resources.Load<QuestViewer>(GameConstants.QuestViever);
        QuestViewer questViewer = Object.Instantiate(viewerPrefab, _uiRoot);
        questViewer.Init(_tutorial);
        _tutorial.RunNextQuest();
    }

    public void CreateTower()
    {
        TowerRenderer prefab = Resources.Load<TowerRenderer>(GameConstants.Tower);
        _tower = Object.Instantiate(prefab);
        _floors = _tower.Floors.ToList();
    }

    public void ClearSpawners()
    {
        _spawnerService.DestroySpawners();
    }
}