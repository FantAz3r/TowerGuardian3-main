using UnityEngine;

public class MoveQuest : Quest
{
    private PlayerMover _mover;
    public MoveQuest(PlayerMover mover)
    {
        _mover = mover;
    }

    public override QuestType GetQuestType()
    {
        return QuestType.Move;
    }

    public override void Run()
    {
        Debug.Log("MoveQuest started");
        _mover.Moved += Complete;
    }

    public override void Complete()
    {
        _mover.Moved -= Complete;
        CompleteQuest();
    }
}
