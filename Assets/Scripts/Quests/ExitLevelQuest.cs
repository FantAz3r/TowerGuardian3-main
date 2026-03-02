using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitLevelQuest : Quest
{
    private Portal _portal;
    private ICoroutineRunner _coroutineRunner;

    public override QuestType GetQuestType() => QuestType.GetOut;
    public override Vector3 TryGetTarget() => _portal.transform.position;

    public ExitLevelQuest(List<Portal> portals)
    {
        _portal = portals.First();
        _coroutineRunner = ServiceLocator.Get<ICoroutineRunner>();
    }

    public override void Run()
    {
        base.Run();
        _portal.gameObject.SetActive(true);
        _portal.CanExit(true);
        _portal.Entered += Complete; 
        _coroutineRunner.StartCoroutine(TimeRoutine());
    }

    private IEnumerator TimeRoutine()
    {
        CurrentTime = Config.TimeLimit;
        QuestViewer.ActivateWarning();

        while (CurrentTime >= 0)
        {
            CurrentTime -= Time.deltaTime;
            base.UpdateTime();
            yield return null;
        }

        QuestViewer.DeactivateWarning();
        base.Fail();
    }

    public override void Complete()
    {
        QuestViewer.DeactivateWarning();
        _portal.Entered -= Complete;
        base.Complete();
    }
}
