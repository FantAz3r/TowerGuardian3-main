using UnityEngine;

public interface IGameConditionService : IService
{
    bool IsLevelEnded { get; }
    bool IsEndLevelWindowOpen { get; }

    void OnWin();
    void OnLouse(GameObject louseReason = null);
    void OnStart(Portal portal);
    void SetLevelEnded(bool isLevelEnded = true);
    void SetEndLevelWindowOpen(bool isEndLevelWindowOpen);
}
