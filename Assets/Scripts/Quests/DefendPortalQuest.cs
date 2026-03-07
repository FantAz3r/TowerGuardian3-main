using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefendPortalQuest : Quest
{
    private Portal _portal;
    private ICoroutineRunner _coroutineRunner;
    public override QuestType GetQuestType() => QuestType.DefendPortal;

    public DefendPortalQuest(List<Portal> portals)
    {
        _portal = portals.First();
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
    }

    public override void Run()
    {
        base.Run();
        CanStop = false;
        _portal.EnemyEntered += UpdateProgress;
        _portal.CanExit(false);
        _coroutineRunner.StartCoroutine(TimeRoutine());
    }

    public override void UpdateProgress()
    {
        CurrentValue++;
        base.UpdateProgress();

        if (CurrentValue >= Config.TargetValue)
        {
            Fail();
        }
    }

    private IEnumerator TimeRoutine()
    {
        CurrentTime = Config.TimeLimit;

        while (CurrentTime >= 0)
        {
            CurrentTime -= Time.deltaTime;
            base.UpdateTime();
            yield return null;
        }

        Complete();
    }

    public override void Fail()
    {
        CanStop = true;
        _portal.EnemyEntered -= UpdateProgress;
        base.Fail();
    }

    public override void Complete()
    {
        CanStop = true;
        _portal.CanExit(false);
        _portal.EnemyEntered -= UpdateProgress;
        base.Complete();
    }
}

