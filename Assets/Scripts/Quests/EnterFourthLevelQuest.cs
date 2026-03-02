using UnityEngine;
using YG;

public class EnterFourthLevelQuest : Quest
{
    private Portal _portalLevel4;
    public override QuestType GetQuestType() => QuestType.EnterLevel4;

    public EnterFourthLevelQuest(Portal portal)
    {
        _portalLevel4 = portal;
    }

    public override Vector3 TryGetTarget()
    {
        return _portalLevel4.transform.position;
    }

    public override void Run()
    {
        base.Run();

        foreach (var levelData in YG2.saves.LevelsProgress)
        {
            if (levelData.Level == LevelID.Level4 && levelData.IsComplite)
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
