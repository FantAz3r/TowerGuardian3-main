public class CollectQuest : Quest
{
    private readonly Inventory _inventory;
    public override QuestType GetQuestType() => QuestType.Collect;

    public CollectQuest(Inventory inventory)
    {
        _inventory = inventory;
    }

    public override void Run()
    {
        base.Run();
        _inventory.WoodCollected += UpdateProgress;
    }

    public override void Complete()
    {
        base.Complete();
        _inventory.WoodCollected -= UpdateProgress;
    }

    public override void UpdateProgress()
    {
        CurrentValue++;
        base.UpdateProgress();

        if(CurrentValue >= Config.TargetValue)
        {
            Complete();
        }
    }
}
