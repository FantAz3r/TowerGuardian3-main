public class GameCompleteQuest : Quest
{
    public override QuestType GetQuestType() => QuestType.GameComplete;

    public override void Run()
    {
        base.Run();
    }
}
