using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitLevelQuest : Quest
{
    private Portal _portal;

    public ExitLevelQuest(List<Portal> portals)
    {
        _portal = GetExit(portals);
    }

    public override QuestType GetQuestType() => QuestType.GetOut;
    public override Vector3 TryGetTarget() => _portal.transform.position;
   

    public override void Run()
    {
        _portal.Entered += Complete;
    }

    public override void Complete()
    {
        _portal.Entered -= Complete;
        base.Complete();
    }

    private Portal GetExit(List<Portal> portals)
    {
        return portals.First();
    }
}
