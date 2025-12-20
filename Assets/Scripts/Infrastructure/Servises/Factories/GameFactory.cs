using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameFactory
{
    private GameUI _uiRoot;
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

    private QuestBuilder _questBuilder;
    private List<CardButton> _buttons = new();
    private ScoreCounter _scoreCounter;

    private PortalFactory _portalFactory;
    private List<Portal> _levelExits = new();
    private GameStateMachine _stateMachine;
    private DayCycle _cycle;
    private TowerRenderer _tower;
    private List<Floor> _floors = new();
    private TowerDoor _towerDoor;
    private StairsTrigger _stairsTrigger;

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

        foreach (var parent in parentObjects)
        {
            List<SpawnbleEntity> resourceItems = parent.GetComponentsInChildren<SpawnbleEntity>().ToList();

            foreach (var item in resourceItems)
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

        spawnerService.RegisterSpawner(new PieceSpawner(resourceData));
        spawnerService.RegisterSpawner(new DamageNumberSpawner(prefab));
        spawnerService.RegisterSpawner(new EffectSpawner(effectData));
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

    public void CreateScoreCounter()
    {
        _scoreCounter = new ScoreCounter(_player, _levelConfig, _cycle);
    }

    public void CreateHUD()
    {
        GameUI prefab = Resources.Load<GameUI>(GameConstants.GameUI);
        _uiRoot = Object.Instantiate(prefab);

        _uiRoot.PauseUI.Init(_stateMachine);
        _uiRoot.SwichDamageNumbers.Init(_spawnerService);
        _uiRoot.ResourceViewer.Init(_inventory);

        _uiRoot.PlayerHealthViewer.Init(_health);
        _uiRoot.LevelViewer.Init(_experience);
        _uiRoot.AbilityPanel.Init(_allAbilities);
        _uiRoot.WeaponPanel.Init(_cardHolder, _weaponFactory, _attacker);
    }

    public void InitUIWindows()
    {
        _uiRoot.Shop.Init(_inventory);
        _uiRoot.Sell.Init(_inventory, _cardData, _cardHolder);


        _uiRoot.WinLevelMenu.Init(_stateMachine, _scoreCounter, _levelConfig.Level);
        _uiRoot.WinScoreViewer.Init(_scoreCounter);

        _uiRoot.LouseLevelMenu.Init(_stateMachine, _scoreCounter, _levelConfig.Level);
        _uiRoot.LouseLevelMenu.SetPlayerHealth(_health);

        _uiRoot.StartLevelMenu.Init(_stateMachine, _scoreCounter, _levelConfig.Level);
        _uiRoot.StartScoreViewer.Init(_scoreCounter);
    }

    public void CreateCards()
    {
        _cardData = Resources.Load<CardData>(GameConstants.CardData);
    }

    public void CreateCardsSelectionMenu()
    {
        CardData cardData = Resources.Load<CardData>(GameConstants.CardData);
        _uiRoot.CardSelectionMenu.Init(_experience, new CardSelector(cardData), _buttons);
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

    public void CreateActions()
    {
        List<IAction> actions = new List<IAction>();

        actions.Add(_openShopAction);
        actions.Add(_openSellAction);
        actions.Add(_uiRoot.transform.GetComponentInChildren<OpenBuildMenuAction>());

        InteractionObjectFactory interactionObjectFactory = new InteractionObjectFactory(actions);
    }

    public void CreatePortalsFactory()
    {
        _portalFactory = new PortalFactory(_uiRoot.WinLevelMenu, _uiRoot.LouseLevelMenu, _uiRoot.StartLevelMenu);
        _levelExits = _portalFactory.Create(_levelConfig.PortalData, _floors);
    }

    public void CreatePlatform()
    {
        Platform prefab = Resources.Load<Platform>(GameConstants.Platform);

        Platform shopPlatform = Object.Instantiate(prefab, new Vector3(20, -12.262f, -87), Quaternion.identity);
        shopPlatform.Init(_openShopAction);

        Platform sellPlatform = Object.Instantiate(prefab, new Vector3(32.5f, -12.262f, -87), Quaternion.identity);
        sellPlatform.Init(_openSellAction);
    }

    public void CreateQuests()
    {
        _questBuilder = new QuestBuilder(_mover, _attacker, _inventory, _cardHolder, _detector, _levelExits, _towerDoor, _stairsTrigger);
    }

    public void CreateTutorial()
    {
        QuestData questData = Resources.Load<QuestData>(GameConstants.QuestData);
        Tutorial tutorialPrefab = Resources.Load<Tutorial>(GameConstants.Tutorial);

        Tutorial tutorial = Object.Instantiate(tutorialPrefab);
        tutorial.Init(_questBuilder, questData, _levelConfig.Quests);
        _portalFactory.SetQuests(tutorial, _levelConfig.Level);

        _uiRoot.QuestViewer.Init(tutorial);
        tutorial.RunNextQuest();
    }

    public void CreateTower()
    {
        TowerRenderer prefab = Resources.Load<TowerRenderer>(GameConstants.Tower);
        _tower = Object.Instantiate(prefab);
        _floors = _tower.Floors.ToList();

        _towerDoor = _tower.GetComponentInChildren<TowerDoor>();
        UIDummy dummy = _tower.GetComponentInChildren<UIDummy>();
        _stairsTrigger = dummy.GetComponent<StairsTrigger>();
    }

    public void ClearSpawners()
    {
        _spawnerService.DestroySpawners();
    }
}