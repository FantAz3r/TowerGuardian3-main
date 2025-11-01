using UnityEngine;

public class MoveQuest : Quest
{
    private PlayerMover _mover;
    public MoveQuest(PlayerMover mover)
    {
        _mover = mover;
    }

    public override void Run()
    {
        base.Run();
        Debug.Log("MoveQuest started");
        _mover.Moved += Complete;
    }

    public override void Complete()
    {
        base.Complete();
        _mover.Moved -= Complete;
    }

    public override QuestType GetQuestType()
    {
        return QuestType.Move;
    }
}
