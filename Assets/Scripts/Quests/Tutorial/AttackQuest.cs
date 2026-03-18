public class AttackQuest : Quest
{
    PlayerAttacker _attacker;
    public override QuestType GetQuestType() => QuestType.Attack;

    public AttackQuest(PlayerAttacker attacker)
    {
        _attacker = attacker;
    }

    public override void Run()
    {
        base.Run();
        _attacker.Hited += Complete;
    }

    public override void Stop()
    {
        base.Stop();
        _attacker.Hited -= Complete;
    }

    public override void Complete()
    {
        _attacker.Hited -= Complete;
        base.Complete();
    }
}