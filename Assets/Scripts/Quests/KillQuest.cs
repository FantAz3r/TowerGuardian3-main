public class KillQuest : Quest
{
    private EnemyDetector _enemyDetector;

    public KillQuest(EnemyDetector enemyDetector) => _enemyDetector = enemyDetector;

    public override QuestType GetQuestType() => QuestType.KillEnemy;

    public override void Run()
    {
        base.Run();
        _enemyDetector.OnEnemyKilled += UpdateProgress;
    }

    public override void Complete()
    {
        _enemyDetector.OnEnemyKilled -= UpdateProgress;
        base.Complete();
    }

    public override void UpdateProgress()
    {
        CurrentValue++;
        base.UpdateProgress();

        if(CurrentValue >= Config.TargetValue)
        {
            Complete();
        }
    }
}