using System;
using UnityEngine;

public interface IQuest
{
    QuestConfig Config { get; }

    event Action OnCompleted;

    QuestType GetQuestType();
    void SetConfig(QuestConfig config);
    void UpdateProgress();
    void Run();
    void Stop();
    void Complete();
    Vector3 TryGetTarget();
}