using UnityEngine;

public class PortalFactory 
{
    private GameStateMachine _gameStateMachine;
    private WinLevelMenu _finishMenu;

    public PortalFactory(GameStateMachine gameStateMachine, WinLevelMenu finishMenu)
    {
        _gameStateMachine = gameStateMachine;
        _finishMenu = finishMenu;
    }

    public void Create(Vector3 buildPoint)
    {
        Portal prefab = Resources.Load<Portal>(GameConstants.Portal);
        Portal portal = Object.Instantiate(prefab, buildPoint, Quaternion.identity);
        portal.Init(_gameStateMachine, _finishMenu);
    }
}
