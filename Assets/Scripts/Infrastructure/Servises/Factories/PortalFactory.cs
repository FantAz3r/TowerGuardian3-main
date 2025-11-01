using UnityEngine;

public class PortalFactory 
{
    private WinLevelMenu _finishMenu;

    public PortalFactory(WinLevelMenu finishMenu)
    {
        _finishMenu = finishMenu;
    }

    public void Create(Vector3 buildPoint)
    {
        Portal prefab = Resources.Load<Portal>(GameConstants.Portal);
        Portal portal = Object.Instantiate(prefab, buildPoint, Quaternion.identity);
        portal.Init(_finishMenu);
    }
}
