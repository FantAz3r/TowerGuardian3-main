using System;

public abstract class Quest : IQuest
{
    public QuestConfig Config { get; private set; }
    private bool _isCompleted;

    public event Action OnCompleted;

    public void SetConfig(QuestConfig config)
    {
        Config = config;
    }

    public abstract QuestType GetQuestType();

    public virtual void Run() { }

    public virtual void Stop() { }

    public virtual void Complete()
    {
        CompleteQuest();
    }

    protected void CompleteQuest()
    {
        if (_isCompleted) return;
        _isCompleted = true;
        OnCompleted?.Invoke();
    }
}
