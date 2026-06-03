public interface IGameFactory : IService
{
    LevelID CurrentLevel { get; }
    LevelConfig LevelConfig { get; }
    DayCycle Cycle { get; }
    Player Player { get; }
    ScoreCounter ScoreCounter { get; }
    QuestStateMachine QuestRunner { get; }
    ISceneContainer SceneContainer { get; }
    CardSelector CardSelector { get; }
    EnemySpawner EnemySpawner { get; }
    BackgroundMusic BackGroundMusic { get; }

    void SetCurrentLevel(LevelID level);
    void SetLevelConfig(LevelID level);
    void SetSceneContainer();
    void CreatePlayer(LevelID previousLevel);
    void CreateSpawners();
    void CreateCamera();
    void CreateEventSystem();
    void CreateCardSelector();
    void CreateScoreCounter();
    void CreateLight();
    void CreateEnemies();
    void CreatePortalsFactory();
    void CreateQuests();
    void CreateQuestRuner();
    void CreateTower();
    void CreateBackgroundSounds();
    void RunLevel();
}
