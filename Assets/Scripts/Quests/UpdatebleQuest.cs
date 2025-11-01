using System;

public abstract class UpdatableQuest : Quest, IUpdatebleQuest
{
    private int _value;
    public int Goal { get; private set; }
    public event Action<int> Updated;

    protected int Value => _value;
    public void UpdateProgress()
    {
        Updated?.Invoke(_value);
    }

    public override QuestType GetQuestType()
    {
        return QuestType.None;
    }
   
}
