using System;
using UnityEngine;

public interface IGameConditionService : IService
{
    public event Action LevelComplited, LevelLoused, LevelStarted;
    void OnWin();
    void OnLouse(GameObject louseReason = null);
    void OnStart(Portal portal);
}
