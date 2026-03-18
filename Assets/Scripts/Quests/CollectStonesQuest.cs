public class CollectStonesQuest : Quest
{
    private readonly Inventory _inventory;
    public override QuestType GetQuestType() => QuestType.CollectStones;

    public CollectStonesQuest(Inventory inventory)
    {
        _inventory = inventory;
    }

    public override void Run()
    {
        base.Run();
        _inventory.StoneCollected += UpdateProgress;
        CurrentValue--;
    }

    public override void Complete()
    {
        base.Complete();
        _inventory.StoneCollected -= UpdateProgress;
    }

    public override void Stop()
    {
        _inventory.StoneCollected -= UpdateProgress;
        base.Stop();
    }

    public override void UpdateProgress()
    {
        CurrentValue++;
        base.UpdateProgress();

        if (CurrentValue >= Config.TargetValue)
        {
            Complete();
        }
    }
}
