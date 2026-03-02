using UnityEngine;

public interface IGameConditionService : IService
{
    void OnWin();
    void OnLouse(GameObject louseReason = null);
    void OnStart(Portal portal);
}
