using System;
using TMPro;
using UnityEngine;

public abstract class Quest : IQuest
{
    private QuestConfig _questConfig;

    public QuestConfig Config => _questConfig;

    public event Action OnCompleted;
    public virtual void SubscribeEvents(Action<int> onUpdated) { }
    public virtual void UnsubscribeEvents(Action<int> onUpdated) { }

    public void SetConfig(QuestConfig config)
    {
        _questConfig = config;
    }

    public abstract QuestType GetQuestType();

    public virtual void Run()
    {
    }

    public virtual void Stop()
    {
    }

    public virtual void Complete()
    {
        OnCompleted?.Invoke();
    }
}
