using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class GameFactory
{
    private Transform _uiRoot;

    private Transform _player;
    private PlayerExperience _experience;
    private Inventory _inventory;
    private PlayerAttacker _attacker;
    private Health _health;
    private AttackZone _attackZone;
    private PlayerMover _mover;
    private PlayerCardConfigContainer _cardHolder;

    private WeaponFactory _weaponFactory;
    private AllCardConfigs _cards;
    private Tutorial _tutorial;
    private AllAbilities _allAbilities;
    private List<CardButton> _buttons = new List<CardButton>();

    private QuestBuilder _questBuilder;

    private DayCycle _cycle;
    private GameStateMachine _stateMachine;
    private OpenShopAction _opebShopAction;
    private Shop _shop;
    private WinLevelMenu _finishMenu;

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

    public void CreatePlayer()
    {
        Player prefab = Resources.Load<Player>(GameConstants.Player);
        _player = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity).transform;
        _player.GetComponentInChildren<PlayerMover>().Init(_inputService);
        _attacker = _player.GetComponentInChildren<PlayerAttacker>();
        _attacker.Init(_spawnerService);
        _inventory = _player.GetComponentInChildren<Inventory>();
        _attackZone = _player.GetComponentInChildren<AttackZone>();
        _experience = _player.GetComponentInChildren<PlayerExperience>();
        _cardHolder = _player.GetComponentInChildren<PlayerCardConfigContainer>();
        _allAbilities = _player.GetComponentInChildren<AllAbilities>();
        _health = _player.GetComponent<Health>();
        _mover = _player.GetComponentInChildren<PlayerMover>();
    }

    public void CreateSpawners(ISpawnerService spawnerService)
    {
        DamageText prefab = Resources.Load<DamageText>(GameConstants.DamageText);
        ResourceData resourceData = Resources.Load<ResourceData>(GameConstants.ResourceData);
        PieceSpawner pieceSpawner = new PieceSpawner(resourceData);
        spawnerService.RegisterSpawner(pieceSpawner);
        spawnerService.RegisterSpawner(new DamageNumberSpawner(prefab));
    }

    public void CreateWeaponFactory()
    {
        _weaponFactory = new WeaponFactory(_player, _attackZone);
    }

    public void CreateCamera()
    {
        CameraFollower prefab = Resources.Load<CameraFollower>(GameConstants.MainCamera);
        CameraFollower camera = Object.Instantiate(prefab);
        camera.Init(_player);
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
        AllCardConfigs prefab = Resources.Load<AllCardConfigs>(GameConstants.AllCards);
        AllCardConfigs cards = Object.Instantiate(prefab);
        cards.Init(_cardHolder);
        _cards = cards;
    }

    public void CreateCardsSelectionMenu()
    {
        CardSelectionMenu prefab = Resources.Load<CardSelectionMenu>(GameConstants.CardSelectionMenu);
        Transform container = _uiRoot.transform;
        CardSelectionMenu panel = Object.Instantiate(prefab, container);
        panel.Init(_timeService, _experience, new CardSelector(_cards, _cardHolder), _buttons);
    }

    public void CreateCardButtons()
    {
        int cardsCount = 3;
        CardButton prefab = Resources.Load<CardButton>(GameConstants.CardViewer);
        Transform container = _uiRoot.transform;

        for (int i = 0; i < cardsCount; i++)
        {
            CardButton button = Object.Instantiate(prefab, container);
            button.Init(_cards, new List<ICardFactory> { _weaponFactory });
            _buttons.Add(button);
        }
    }

    public void CreateLight(LevelID level)
    {
        DayCycle prefab = Resources.Load<DayCycle>(GameConstants.DirectionLight);
        _cycle = Object.Instantiate(prefab);
        _cycle.Init(level);
    }

    public void CreateEnemies(LevelID level)
    {
        EnemySpawner prefab = Resources.Load<EnemySpawner>(GameConstants.EnemySpawner);
        EnemySpawner spawner = Object.Instantiate(prefab);
        spawner.Init(_player.transform, _cycle, level, _spawnerService);
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

        actions.Add(_opebShopAction);
        actions.Add(_uiRoot.GetComponentInChildren<OpenBuildMenuAction>());

        InteractionObjectFactory interactionObjectFactory = new InteractionObjectFactory(actions);
    }

    public void CreatePortalsFactory()
    {
        PortalFactory portalFactory = new PortalFactory(_finishMenu);
        portalFactory.Create(new Vector3(0, 0, 20));
    }

    public void CreatePlatform()
    {
        Platform prefab = Resources.Load<Platform>(GameConstants.Platform);
        Platform platform = Object.Instantiate(prefab, new Vector3(0, 0, 18), Quaternion.identity);
        platform.Init(_opebShopAction);
    }

    public void CreateShop()
    {
        Shop prefab = Resources.Load<Shop>(GameConstants.Shop);
        _shop = Object.Instantiate(prefab, _uiRoot.transform);
        _opebShopAction = _uiRoot.GetComponentInChildren<OpenShopAction>(true);
        _shop.Init(_inventory, _cards);
    }

    public void CreateEndLevelMenu(LevelID level)
    {
        WinLevelMenu prefab = Resources.Load<WinLevelMenu>(GameConstants.WinMenu);
        WinLevelMenu menu = Object.Instantiate(prefab, _uiRoot.transform);
        _finishMenu = menu;
        _finishMenu.Init(_stateMachine, level);
    }

    public void CreateQuests()
    {
        _questBuilder =  new QuestBuilder(_mover, _attacker, _inventory, _cardHolder, _shop);
    }

    public void CreateTutorial()
    {
        Tutorial tutorialPrefab = Resources.Load<Tutorial>(GameConstants.Tutorial);
        _tutorial = Object.Instantiate(tutorialPrefab);
        _tutorial.Init(_questBuilder);
       
    }

    public void CreateQuestViewer()
    {
        QuestViewer viewerPrefab = Resources.Load<QuestViewer>(GameConstants.QuestViever);
        QuestViewer questViewer = Object.Instantiate(viewerPrefab, _uiRoot);
        questViewer.Init(_tutorial);
        _tutorial.RunNextQuest();
    }

    public void ClearSpawners()
    {
        _spawnerService.DestroySpawners();
    }
}