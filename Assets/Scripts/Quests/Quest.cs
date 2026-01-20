using System;
using UnityEngine;

public abstract class Quest : IQuest
{
    private int _goal;
    private int _currentValue = 0;
    public QuestConfig Config { get; private set; }

    public int Goal => _goal;

    private bool _isCompleted;

    public event Action OnCompleted;
    public event Action<int> Updated;

    public void SetConfig(QuestConfig config)
    {
        Config = config;
        _goal = config.TargetValue;
    }

    public abstract QuestType GetQuestType();

    public virtual void Run() { }

    public virtual Vector3 TryGetTarget()
    {
        return Vector3.zero;
    }

    public virtual void UpdateProgress()
    {
        _currentValue++;
        Updated?.Invoke(_currentValue);

        if (_currentValue >= _goal)
        {
            CompleteQuest();
            return;
        }
    }

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