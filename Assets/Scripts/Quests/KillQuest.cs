public class KillQuest : Quest, IQuest
{
    private EnemyDetector _enemyDetector;

    public KillQuest(EnemyDetector enemyDetector)
    {
        _enemyDetector = enemyDetector;
    }

    public override QuestType GetQuestType()
    {
        return QuestType.KillEnemy;
    }

    public override void Run()
    {
        _enemyDetector.OnEnemyKilled += UpdateProgress;
    }

    public override void Complete()
    {
        _enemyDetector.OnEnemyKilled -= UpdateProgress;
        base.Complete();
    }
}