using System;

public interface IQuest  
{
    QuestConfig Config { get; }

    event Action OnCompleted;
    void Run();
    void Stop();
    void Complete();
}
