using System;

public interface IUpdatableQuest : IQuest
{
    int Goal { get; }
    event Action<int> Updated;
    void UpdateProgress();
    void Set(int goal);
}