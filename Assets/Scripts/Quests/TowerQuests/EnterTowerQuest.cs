public class EnterTowerQuest : Quest
{
    private TowerDoor _door;

    public override QuestType GetQuestType() => QuestType.EnterTower;

    public EnterTowerQuest(TowerDoor door)
    {
        _door = door;
    }

    public override void Run()
    {
        base.Run();
        _door.Opened += Complete;
    }

    public override void Complete()
    {
        base.Complete();
        _door.Opened -= Complete;
    }
}
