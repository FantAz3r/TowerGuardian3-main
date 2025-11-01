using System;

public interface IUpdatebleQuest : IQuest
{
    int Goal { get; }

    event Action<int> Updated;

    void UpdateProgress();
}