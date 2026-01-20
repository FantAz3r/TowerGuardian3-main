using System;

public class DefendPortalQuest : Quest
{
    private Portal _portal;
    private ICoroutineRunner _coroutineRunner;
    private int _currentEnemiesInside = 0;

    public override event Action<int> Updated;
    public override QuestType GetQuestType() => QuestType.DefendPortal;

    public DefendPortalQuest(Portal portal, ICoroutineRunner coroutineRunner)
    {
        _portal = portal;
        _coroutineRunner = coroutineRunner;
    }

    public override void Run()
    {
        _coroutineRunner.StopCoroutine(_timeLimit);
        _portal.EnemyEntered += OnEnemyEntered;
    }

    public override void Complete()
    {
        _portal.EnemyEntered -= OnEnemyEntered;
        base.Complete();
    }

    private void OnEnemyEntered()
    {
        _currentEnemiesInside++;
        UpdateProgress();
    }

    public override void UpdateProgress()
    {

    }
}
