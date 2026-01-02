using UnityEngine;

public class EnterFirstLevelQuest : Quest
{
    private Portal _portalLevel1;
    public override QuestType GetQuestType() => QuestType.EnterFirstLevel;

    public EnterFirstLevelQuest(Portal portal)
    {
        _portalLevel1 = portal;
    }

    public override Vector3 TryGetTarget()
    {
        return _portalLevel1.transform.position;
    }

    public override void Run()
    {
        base.Run();
        _portalLevel1.Exited += Complete;
    }

    public override void Complete()
    {
        _portalLevel1.Exited -= Complete;
        base.Complete();
    }
}
