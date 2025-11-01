using System;
using TMPro;
using UnityEngine;

public abstract class Quest : IQuest
{
    [SerializeField] private QuestConfig _config;

    public Sprite Sprite => _config.Image;
    public string Description => _config.Description;
    public QuestType QuestType => _config.QuestType;

    public event Action OnCompleted;
    public virtual void SubscribeEvents(Action<int> onUpdated) { }
    public virtual void UnsubscribeEvents(Action<int> onUpdated) { }

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
