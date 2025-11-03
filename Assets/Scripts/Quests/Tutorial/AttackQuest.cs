using System.Collections;
using System.Collections.Generic;
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
        base.Complete();
        _attacker.Hited -= Complete;
    }
}
