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
        _inventory.WoodCollected += UpdateProgress;
    }

    public override void Stop()
    {
        _inventory.WoodCollected -= UpdateProgress;
    }

    public override void Complete()
    {
        Stop();
        base.Complete();
    }
}
