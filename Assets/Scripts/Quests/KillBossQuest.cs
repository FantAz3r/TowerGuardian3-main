using UnityEngine;

public class KillBossQuest : Quest
{
    private Enemy _boss;
    private BossHealthViewer _healthViewer;
    private Transform _bossSpawnPoint;
    private EnemySpawner _enemySpawner;
    private BackgroundMusic _backGroundMusic;
    private HUD _hud;
    private ISceneContainer _sceneContainer;

    public KillBossQuest()
    {
        _backGroundMusic = ServiceLocator.Get<IGameFactory>().BackGroundMusic;
        _enemySpawner = ServiceLocator.Get<IGameFactory>().EnemySpawner;
        _boss = Resources.Load<Enemy>(GameConstants.FinalBoss);
        _sceneContainer = ServiceLocator.Get<IGameFactory>().SceneContainer;
    }

    public override QuestType GetQuestType() => QuestType.KillBoss;

    public override void Run()
    {
        _backGroundMusic.StartBattleMusic();

        foreach (var item in _sceneContainer.QuestObjects)
        {
            if (item.TryGetComponent(out BossSpawnPoint point))
            {
                _bossSpawnPoint = point.transform;
            }
        }

        base.Run();
        _hud = ServiceLocator.Get<IUIFactory>().HUD;

        WaveViewer waveViewer = _hud.GetComponentInChildren<WaveViewer>();
        waveViewer.StopWaves();
        waveViewer.gameObject.SetActive(false);

        _enemySpawner.ClearEnemies();
        _boss = _enemySpawner.SpawnBoss(_boss, _bossSpawnPoint.position);
        _healthViewer = ServiceLocator.Get<IWindowService>().Open(WindowType.BossHealth) as BossHealthViewer;
        _healthViewer.Init(_boss.Health);
        _boss.Health.Died += Complete;
    }

    public override void Stop()
    {
        _boss.Health.Died -= Complete;
        base.Stop();
    }

    public override void Complete()
    {
        _boss.Health.Died -= Complete;
        base.Complete();
    }
}
