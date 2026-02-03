using System;

public interface IGameConditionService : IService
{
    public event Action LevelComplited, LevelLoused, LevelStarted;
    void OnWin();
    void OnLouse();
    void OnStart(Portal portal);
}
