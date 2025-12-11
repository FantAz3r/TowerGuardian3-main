using UnityEngine;

public class AttackQuest : Quest
{
    PlayerAttacker _attacker;

    public AttackQuest(PlayerAttacker attacker)
    {
        _attacker = attacker;
    }

    public override void Run()
    {
        base.Run();
        Debug.Log("Attack Quest started");
        _attacker.Hited += Complete;
    }

    public override QuestType GetQuestType()
    {
        return QuestType.Attack;
    }

    public override void Complete()
    {
        _attacker.Hited -= Complete;
        base.Complete();
    }
}
