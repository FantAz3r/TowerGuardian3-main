using UnityEngine;
using YG;

public class EnterThirdLevelQuest : Quest
{
    private Portal _portalLevel3;

    public EnterThirdLevelQuest(Portal portal)
    {
        _portalLevel3 = portal;
    }

    public override QuestType GetQuestType() => QuestType.EnterLevel3;

    public override Vector3 TryGetTarget()
    {
        return _portalLevel3.transform.position;
    }

    public override void Run()
    {
        base.Run();

        if (YG2.saves.LevelsProgress == null) return;

        foreach (var levelData in YG2.saves.LevelsProgress)
        {
            if (levelData.Level == LevelID.Level3 && levelData.IsComplite)
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
