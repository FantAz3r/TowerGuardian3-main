using System.Collections.Generic;
using System.Linq;

public class ExitLevelQuest : Quest
{
    private Portal _portal;

    public ExitLevelQuest(List<Portal> portals)
    {
        _portal = GetExit(portals);
    }

    public override QuestType GetQuestType()
    {
        return QuestType.GetOut;
    }

    public override void Run()
    {
        _portal.Exited += Complete;
    }

    public override void Complete()
    {
        _portal.Exited -= Complete;
        base.Complete();
    }

    private Portal GetExit(List<Portal> portals)
    {
        return portals.First();
    }
}
