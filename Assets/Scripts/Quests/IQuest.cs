using System;

public interface IQuest  
{
    QuestConfig Config { get; }
    int Goal { get; }

    event Action<int> Updated;

    event Action OnCompleted;

    void UpdateProgress();
    void Run();
    void Stop();
    void Complete();
}
