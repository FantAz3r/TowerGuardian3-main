using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitLevelQuest : Quest
{
    private Portal _portal;
    public override QuestType GetQuestType() => QuestType.GetOut;
    public override Vector3 TryGetTarget() => _portal.transform.position;

    public ExitLevelQuest(List<Portal> portals)
    {
        _portal = portals.First();
    }

    public override void Run()
    {
        base.Run();
        _portal.gameObject.SetActive(true);
        _portal.CanExit(true);
        _portal.Entered += Complete;
    }

    public override void Complete()
    {
        _portal.Entered -= Complete;
        base.Complete();
    }
}
