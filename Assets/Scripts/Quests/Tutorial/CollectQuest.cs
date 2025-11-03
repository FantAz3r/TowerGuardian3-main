public class CollectQuest : UpdatableQuest
{
    private readonly Inventory _inventory;

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

    public override QuestType GetQuestType()
    {
        return QuestType.Collect;
    }
}
