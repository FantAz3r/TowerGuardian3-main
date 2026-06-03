using UnityEngine;
using YG;

public class EnterFinalLevelQuest : Quest
{
    private Portal _portalLevel5;

    public EnterFinalLevelQuest(Portal portal)
    {
        _portalLevel5 = portal;
    }
    public override QuestType GetQuestType() => QuestType.EnterLevel5;

    public override Vector3 TryGetTarget()
    {
        return _portalLevel5.transform.position;
    }

    public override void Run()
    {
        base.Run();

        if (YG2.saves.LevelsProgress == null) return;

        foreach (var levelData in YG2.saves.LevelsProgress)
        {
            if (levelData.Level == LevelID.Level5 && levelData.IsComplite)
            {
                Complete();
                break;
            }
        }
    }

    public override void Complete()
    {
        base.Complete();
    }
}
