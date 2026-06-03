using System;
using UnityEngine;

public interface IQuest
{
    event Action OnCompleted;
    bool CanStop { get; }
    QuestConfig Config { get; }

    QuestType GetQuestType();
    void SetConfig(QuestConfig config);
    void UpdateProgress();
    void Run();
    void Stop();
    void Complete();
    Vector3 TryGetTarget();
}