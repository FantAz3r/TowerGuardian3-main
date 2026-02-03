using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameFactory : IGameFactory
{
    private Tower _tower;
    private EnemySpawner _enemySpawner;
    private Tutorial _tutorial;
    private QuestBuilder _questBuilder;
    private PortalSwitcher _portalSwitcher;
    private ISceneContainer _sceneContainer;
    private ISpawnerService _spawnerService;

    public LevelID CurrentLevel { get; private set; }
    public LevelConfig LevelConfig { get; private set; }
    public DayCycle Cycle { get; private set; }
    public Player Player { get; private set; }
    public ScoreCounter ScoreCounter { get; private set; }

    public GameFactory()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
    }

    public void SetCurrentLevel(LevelID level)
    {
        CurrentLevel = level;
    }

    public void SetLevelConfig(LevelID level)
    {
        LevelData levelData = Resources.Load<LevelData>(GameConstants.LevelData);

        foreach (var levelInfo in levelData.LevelInfos)
        {
            if (levelInfo.LevelID == level)
            {
                LevelConfig = levelInfo.Config;
            }
        }
    }

    public void SetSceneContainer()
    {
        var scene = SceneManager.GetActiveScene();
        List<GameObject> parentObjects = scene.GetRootGameObjects().ToList();

        foreach (var parent in parentObjects)
        {
            if (parent.TryGetComponent(out SceneContainer sceneContainer))
            {
                _sceneContainer = sceneContainer;
            }
        }
    }

    public void CreatePlayer(LevelID previousLevel)
    {
        PlayerSpawnPointSeter spawner = new PlayerSpawnPointSeter(Resources.Load<PlayerSpawnPoints>(GameConstants.PlayerSpawnPoints));
        Player = Object.Instantiate(Resources.Load<Player>(GameConstants.Player), spawner.GetSpawnPoint(LevelConfig, previousLevel), Quaternion.identity);
    }

    public void CreateSpawners()
    {
        DamageText textObject = Resources.Load<DamageText>(GameConstants.DamageText);
        ResourceData resourceData = Resources.Load<ResourceData>(GameConstants.ResourceData);
        EffectData effectData = Resources.Load<EffectData>(GameConstants.EffectData);
        SoundData soundData = Resources.Load<SoundData>(GameConstants.SoundData);
        SoundObject soundObject = Resources.Load<SoundObject>(GameConstants.SoundObject);

        _spawnerService.RegisterSpawner(new SoundSpawner(soundData, soundObject));
        _spawnerService.RegisterSpawner(new PieceSpawner(resourceData));
        _spawnerService.RegisterSpawner(new DamageNumberSpawner(textObject));
        _spawnerService.RegisterSpawner(new EffectSpawner(effectData));
    }

    public void CreateFocusController()
    {
        ApplicationFocusController focusController = Object.Instantiate(Resources.Load<ApplicationFocusController>(GameConstants.FocusController));
    }

    public void CreateCamera()
    {
        CameraFollower camera = Object.Instantiate(Resources.Load<CameraFollower>(GameConstants.MainCamera));
        TransparencyTrigger transparencyTrigger = camera.GetComponent<TransparencyTrigger>();

        transparencyTrigger.Init(Player.transform);
        camera.Init(Player.transform);
    }

    public void CreateEventSystem()
    {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    public void CreateScoreCounter()
    {
        ScoreCounter = new ScoreCounter(Player, LevelConfig);
    }

    public void CreateLight()
    {
        Cycle = Object.Instantiate(Resources.Load<DayCycle>(GameConstants.DirectionLight));
        Cycle.Init(LevelConfig);
    }


    public void CreateEnemies()
    {
        _enemySpawner = Object.Instantiate(Resources.Load<EnemySpawner>(GameConstants.EnemySpawner));
        _enemySpawner.Init(Player, Cycle, LevelConfig);
    }

    public void CreatePortalsFactory()
    {
        _portalSwitcher = new PortalSwitcher();
        _portalSwitcher.Init(_sceneContainer.Portals);
    }

    public void CreateQuests()
    {
        if (LevelConfig.Level == LevelID.Tower)
        {
            _questBuilder = new QuestBuilder(Player, _sceneContainer.Portals, _tower.Door, _tower.StairsFirstFloor);
        }
        else
        {
            _questBuilder = new QuestBuilder(Player, _sceneContainer.Portals);
        }
    }

    public void CreateTutorial()
    {
        _tutorial = Object.Instantiate(Resources.Load<Tutorial>(GameConstants.Tutorial));
        _tutorial.Init(_questBuilder, LevelConfig.Level, LevelConfig.Quests);
        Player.QuestPointer.Init(Player.transform, _tutorial);
    }

    public void CreateTower()
    {
        _tower = Object.Instantiate(Resources.Load<Tower>(GameConstants.Tower));
        _sceneContainer = _tower;
    }

    public void CreateBackgroundSounds()
    {
        BackGroundMusic backGroundMusic = Object.Instantiate(Resources.Load<BackGroundMusic>(GameConstants.BackGroundMusic));
    }

    public void RunLevel()
    {
        _tutorial?.RunQuests();
        _enemySpawner?.StartSpawn();
        Cycle?.StartDayCycle();
    }
}

public interface IGameFactory : IService
{
    LevelID CurrentLevel { get; }
    LevelConfig LevelConfig { get; }
    DayCycle Cycle { get; }
    Player Player { get; }
    ScoreCounter ScoreCounter { get; }

    void SetCurrentLevel(LevelID level);
    void SetLevelConfig(LevelID level);
    void SetSceneContainer();
    void CreatePlayer(LevelID previousLevel);
    void CreateSpawners();
    void CreateFocusController();
    void CreateCamera();
    void CreateEventSystem();
    void CreateScoreCounter();
    void CreateLight();
    void CreateEnemies();
    void CreatePortalsFactory();
    void CreateQuests();
    void CreateTutorial();
    void CreateTower();
    void CreateBackgroundSounds();
    void RunLevel();
}
