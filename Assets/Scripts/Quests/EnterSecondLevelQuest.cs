using UnityEngine;

public class EnterSecondLevelQuest : Quest
{
    private Portal _portalLevel2;
    public override QuestType GetQuestType() => QuestType.EnterLevel2;

    public EnterSecondLevelQuest(Portal portal)
    {
        _portalLevel2 = portal;
    }

    public override Vector3 TryGetTarget()
    {
        return _portalLevel2.transform.position;
    }

    public override void Run()
    {
        base.Run();
        _portalLevel2.Entered += Complete;
    }

    public override void Complete()
    {
        _portalLevel2.Entered -= Complete;
        base.Complete();
    }
}
