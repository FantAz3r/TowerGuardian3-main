using UnityEngine;

public class PortalFactory 
{
    private GameStateMachine _gameStateMachine;

    public PortalFactory(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void Create(Vector3 buildPoint)
    {
        Portal prefab = Resources.Load<Portal>(GameConstants.Portal);
        Portal portal = Object.Instantiate(prefab, buildPoint, Quaternion.identity);
        portal.Init(_gameStateMachine);
    }
}
