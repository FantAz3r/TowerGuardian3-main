using System;
using UnityEngine;

public interface IQuest  
{
    Sprite Sprite { get; }
    string Description { get; }

    event Action OnComplited;
    void Run();
    void Update();
    void Stop();
    void Complite();
}
