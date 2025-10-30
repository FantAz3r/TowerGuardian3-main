using System;
using UnityEngine;

public class MoveQuest : MonoBehaviour, IQuest
{
    public Sprite Sprite => throw new NotImplementedException();

    public string Description => throw new NotImplementedException();

    public event Action OnComplited;

    public void Complite()
    {
    }

    public void Run()
    {
    }

    public void Stop()
    {
    }

    public void Update()
    {

    }
}
