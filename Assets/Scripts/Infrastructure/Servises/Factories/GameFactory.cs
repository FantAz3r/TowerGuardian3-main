using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private QuestPointer _questPointer;

    private WeaponFactory _weaponFactory;
    private CardData _cardData;
    private AllAbilities _allAbilities;

    private QuestBuilder _questBuilder;
    private List<CardButton> _buttons = new();
    private ScoreCounter _scoreCounter;

    private PortalFactory _portalFactory;
    private List<Portal> _levelExits = new();
    private DayCycle _cycle;

    private Tower _tower;
    private TowerRenderer _towerRenderer;
    private List<Floor> _floors = new();
    private TowerDoor _towerDoor;
    private StairsTrigger _stairsTrigger;

    private OpenShopAction _openShopAction;
    private OpenSellAction _openSellAction;

    private ISpawnerService _spawnerService;

    public GameFactory()
    {
        _spawnerService = ServicesLocator.GetService<ISpawnerService>();
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

    public void CreatePlayer(LevelID previousLevel)
    {
        PlayerSpawnPointSeter spawner = new PlayerSpawnPointSeter(Resources.Load<PlayerSpawnPoints>(GameConstants.PlayerSpawnPoints));
        Player prefab = Resources.Load<Player>(GameConstants.Player);
        _player = Object.Instantiate(prefab, spawner.GetSpawnPoint(_levelConfig, previousLevel), Quaternion.identity);

        _attacker = _player.GetComponentInChildren<PlayerAttacker>();
        _inventory = _player.GetComponentInChildren<Inventory>();
        _attackZone = _player.GetComponentInChildren<AttackZone>();
        _experience = _player.GetComponentInChildren<PlayerExperience>();
        _cardHolder = _player.GetComponentInChildren<PlayerCardConfigContainer>();
        _allAbilities = _player.GetComponentInChildren<AllAbilities>();
        _detector = _player.GetComponentInChildren<EnemyDetector>();
        _health = _player.GetComponent<Health>();
        _mover = _player.GetComponentInChildren<PlayerMover>();
        _questPointer = _player.GetComponentInChildren<QuestPointer>();
    }

    public void CreateSpawners()
    {
        DamageText textObject = Resources.Load<DamageText>(GameConstants.DamageText);
        ResourceData resourceData = Resources.Load<ResourceData>(GameConstants.ResourceData);
        EffectData effectData = Resources.Load<EffectData>(GameConstants.EffectData);
        SoundData soundData = Resources.Load<SoundData>(GameConstants.SoundData);
        SoundObject soundObject = Resources.Load<SoundObject>(GameConstants.SoundObject);

        _spawnerService.RegisterSpawner(new PieceSpawner(resourceData));
        _spawnerService.RegisterSpawner(new DamageNumberSpawner(textObject));
        _spawnerService.RegisterSpawner(new EffectSpawner(effectData));
        _spawnerService.RegisterSpawner(new SoundSpawner(soundData, soundObject));
    }

    public void CreateFocusController()
    {
        ApplicationFocusController prefab = Resources.Load<ApplicationFocusController>(GameConstants.FocusController);
        ApplicationFocusController focusController = Object.Instantiate(prefab);
    }

    public void CreateWeaponFactory()
    {
        _weaponFactory = new WeaponFactory(_player.transform, _attackZone);
    }

    public void CreateCamera()
    {
        CameraFollower prefab = Resources.Load<CameraFollower>(GameConstants.MainCamera);
        CameraFollower camera = Object.Instantiate(prefab);
        TransparencyTrigger transparencyTrigger = camera.GetComponent<TransparencyTrigger>();

        transparencyTrigger.Init(_player.transform);
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

    public void CreateUI()
    {
        GameUI prefab = Resources.Load<GameUI>(GameConstants.GameUI);
        _uiRoot = Object.Instantiate(prefab);
    }

    public void CreateHUD()
    {
        _uiRoot.ResourceViewer.Init(_inventory);
        _uiRoot.PlayerHealthViewer.Init(_health);
        _uiRoot.LevelViewer.Init(_experience);
        _uiRoot.AbilityPanel.Init(_allAbilities, _attacker);
        _uiRoot.WeaponPanel.Init(_cardHolder, _weaponFactory, _attacker);
        _uiRoot.Clock.Init(_cycle);
    }

    public void InitUIWindows()
    {
        _uiRoot.Shop.Init(_inventory);
        _uiRoot.Sell.Init(_inventory, _cardData, _cardHolder, _uiRoot.CardSelectionMenu);
        _uiRoot.PauseUI.Init(_levelConfig.Level);
        _uiRoot.WinLevelMenu.Init(_scoreCounter, _uiRoot, _levelConfig.Level);
        _uiRoot.WinScoreViewer.Init(_scoreCounter);

        _uiRoot.LouseLevelMenu.Init(_scoreCounter, _uiRoot, _levelConfig.Level);
        _uiRoot.LouseLevelMenu.SetPlayerHealth(_health);

        _uiRoot.StartLevelMenu.Init(_scoreCounter, _uiRoot, _levelConfig.Level);
        _uiRoot.StartScoreViewer.Init(_scoreCounter);
    }

    public void CreateCards()
    {
        _cardData = Resources.Load<CardData>(GameConstants.CardData);
    }

    public void CreateCardsSelectionMenu()
    {
        CardData cardData = Resources.Load<CardData>(GameConstants.CardData);
        _uiRoot.CardSelectionMenu.Init(_experience, new CardSelector(cardData), _buttons, _uiRoot);
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
        spawner.Init(_player, _cycle, _levelConfig);
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

    public void InitPlatforms()
    {
        _openShopAction = _uiRoot.Shop.GetComponent<OpenShopAction>();
        _tower.ShopPlatform.Init(_openShopAction, _uiRoot, UIText.Shop);
    }

    public void CreateQuests()
    {
        if (_levelConfig.Level == LevelID.Tower)
        {
            _questBuilder = new QuestBuilder(_mover, _attacker, _inventory, _cardHolder, _detector, _levelExits, _tower.Door, _tower.StairsFirstFloor);
        }
        else
        {
            _questBuilder = new QuestBuilder(_mover, _attacker, _inventory, _cardHolder, _detector, _levelExits);
        }
    }

    public void CreateTutorial()
    {
        QuestData questData = Resources.Load<QuestData>(GameConstants.QuestData);
        Tutorial tutorialPrefab = Resources.Load<Tutorial>(GameConstants.Tutorial);

        Tutorial tutorial = Object.Instantiate(tutorialPrefab);
        tutorial.Init(_levelConfig.Level, _questBuilder, questData, _levelConfig.Quests);

        _questPointer.Init(_player.transform, tutorial);
        _portalFactory.SetQuests(tutorial, _levelConfig.Level);
        _uiRoot.QuestViewer.Init(tutorial);
        tutorial.RunNextQuest();
    }

    public void CreateTower()
    {
        TowerRenderer prefab = Resources.Load<TowerRenderer>(GameConstants.Tower);
        _towerRenderer = Object.Instantiate(prefab);
        _floors = _towerRenderer.Floors.ToList();
        _towerRenderer.TryGetComponent(out Tower tower);
        _tower = tower;
    }

    public void CreateBackgroundSounds()
    {
        BackGroundMusic prefab = Resources.Load<BackGroundMusic>(GameConstants.BackGroundMusic);
        BackGroundMusic backGroundMusic = Object.Instantiate(prefab);
    }

    public void ClearSpawners()
    {
        _spawnerService.DestroySpawners();
    }
}