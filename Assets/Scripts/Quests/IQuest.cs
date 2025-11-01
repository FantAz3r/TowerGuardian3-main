using System;
using UnityEngine;

public interface IQuest  
{
    Sprite Sprite { get; }
    string Description { get; }

    event Action OnCompleted;
    void Run();
    void Stop();
    void Complete();
}
