using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameFactory : IGameFactory
{
    private Scene _scene;
    private Tower _tower;
    private QuestBuilder _questBuilder;
    private PortalSwitcher _portalSwitcher;
    private ISpawnerService _spawnerService;

    public GameFactory()
    {
        _spawnerService = ServiceLocator.Get<ISpawnerService>();
    }

    public EnemySpawner EnemySpawner { get; private set; }
    public BackgroundMusic BackGroundMusic { get; private set; }
    public CardSelector CardSelector { get; private set; }
    public LevelID CurrentLevel { get; private set; }
    public LevelConfig LevelConfig { get; private set; }
    public DayCycle Cycle { get; private set; }
    public Player Player { get; private set; }
    public ScoreCounter ScoreCounter { get; private set; }
    public QuestStateMachine QuestRunner { get; private set; }

    public ISceneContainer SceneContainer { get; private set; }

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
        _scene = SceneManager.GetActiveScene();
        List<GameObject> parentObjects = _scene.GetRootGameObjects().ToList();

        foreach (var parent in parentObjects)
        {
            if (parent.TryGetComponent(out SceneContainer sceneContainer))
            {
                SceneContainer = sceneContainer;
            }
        }
    }

    public void CreatePlayer(LevelID previousLevel)
    {
        PlayerSpawnPointSeter spawner = new PlayerSpawnPointSeter(SceneContainer);
        Player = Object.Instantiate(Resources.Load<Player>(GameConstants.Player), spawner.GetSpawnPoint(LevelConfig.Level, previousLevel), Quaternion.identity);
    }

    public void CreateSpawners()
    {
        DamageText textObject = Resources.Load<DamageText>(GameConstants.DamageText);
        ResourceData resourceData = Resources.Load<ResourceData>(GameConstants.ResourceData);
        EffectData effectData = Resources.Load<EffectData>(GameConstants.EffectData);
        SoundData soundData = Resources.Load<SoundData>(GameConstants.SoundData);
        SoundObject soundObject = Resources.Load<SoundObject>(GameConstants.SoundObject);

        _spawnerService.RegisterSpawner(new ProjectileSpawner());
        _spawnerService.RegisterSpawner(new SoundSpawner(soundData, soundObject));
        _spawnerService.RegisterSpawner(new PieceSpawner(resourceData));
        _spawnerService.RegisterSpawner(new DamageNumberSpawner(textObject));
        _spawnerService.RegisterSpawner(new EffectSpawner(effectData));
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

    public void CreateCardSelector()
    {
        CardSelector = new CardSelector();
    }

    public void CreateScoreCounter()
    {
        ScoreCounter = new ScoreCounter();
    }

    public void CreateLight()
    {
        Cycle = Object.Instantiate(Resources.Load<DayCycle>(GameConstants.DirectionLight));
    }

    public void CreateEnemies()
    {
        EnemySpawner = Object.Instantiate(Resources.Load<EnemySpawner>(GameConstants.EnemySpawner));
    }

    public void CreatePortalsFactory()
    {
        _portalSwitcher = new PortalSwitcher();
        _portalSwitcher.Init(SceneContainer.Portals);
    }

    public void CreateQuests()
    {
        if (LevelConfig.Level == LevelID.Tower)
        {
            _questBuilder = new QuestBuilder(Player, SceneContainer.Portals, _tower.Door, _tower.StairsFirstFloor);
        }
        else
        {
            _questBuilder = new QuestBuilder(Player, SceneContainer.Portals);
        }
    }

    public void CreateQuestRuner()
    {
        QuestRunner = Object.Instantiate(Resources.Load<QuestStateMachine>(GameConstants.Tutorial));
        QuestRunner.Init(_questBuilder, LevelConfig.Level, LevelConfig.Quests);
        Player.QuestPointer.Init();
    }

    public void CreateTower()
    {
        _scene = SceneManager.GetActiveScene();
        List<GameObject> parentObjects = _scene.GetRootGameObjects().ToList();

        foreach (var parent in parentObjects)
        {
            if (parent.TryGetComponent(out Tower sceneContainer))
            {
                _tower = sceneContainer;
                SceneContainer = _tower;
            }
        }
    }

    public void CreateBackgroundSounds()
    {
        BackGroundMusic = Object.Instantiate(Resources.Load<BackgroundMusic>(GameConstants.BackGroundMusic));
    }

    public void RunLevel()
    {
        int maxTweens = 1000;
        int maxSequence = 200;

        DOTween.SetTweensCapacity(maxTweens, maxSequence);
        QuestRunner?.Run();
        Cycle?.StartDayCycle();

        if (LevelConfig.Level != LevelID.Tower)
        {
            EnemySpawner?.StartSpawn();
        }
    }
}