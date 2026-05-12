using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteQuest : Quest
{
    public override QuestType GetQuestType() => QuestType.GameComplete;


    public override void Run()
    {
        base.Run();
    }
}
