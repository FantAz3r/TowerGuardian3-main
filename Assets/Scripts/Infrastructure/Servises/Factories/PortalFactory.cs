using System.Collections.Generic;
using UnityEngine;

public class PortalFactory
{
    private WinLevelMenu _finishMenu;
    private LouseLevelMenu _loseMenu;
    private StartLevelMenu _startMenu;
    private List<Portal> _portals = new();
    private Tutorial _tutorial;

    public PortalFactory(WinLevelMenu finishMenu, LouseLevelMenu loseMenu, StartLevelMenu startMenu)
    {
        _finishMenu = finishMenu;
        _loseMenu = loseMenu;
        _startMenu = startMenu;
    }

    public void SetQuests(Tutorial tutorial)
    {
        _tutorial = tutorial;
        DisablePortals();
        _tutorial.Complited += EnablePortals;
    }

    public List<Portal> Create(PortalData portalData, List<Floor> floors)
    {
        if (floors.Count > 1)
        {
            foreach (var portalInfo in portalData.Infos)
            {
                Floor floor = floors.Find(floor => floor.FloorNumber == portalInfo.Floor);

                if (floor != null)
                {
                    Portal prefab = Resources.Load<Portal>(GameConstants.Portal);
                    Portal portal = Object.Instantiate(prefab, portalInfo.Transform.position, portalInfo.Transform.rotation, floor.transform);
                    portal.Init(_finishMenu, _loseMenu, portalInfo.LevelID, portalInfo.Material, LevelID.Tower, _startMenu);
                    _portals.Add(portal);
                }
                else
                {
                    Debug.LogWarning($"Floor {portalInfo.Floor} not found for portal");
                }
            }
        }
        else
        {
            foreach (var portalInfo in portalData.Infos)
            {
                Portal prefab = Resources.Load<Portal>(GameConstants.Portal);
                Portal portal = Object.Instantiate(prefab, portalInfo.Transform.position, portalInfo.Transform.rotation);
                portal.Init(_finishMenu, _loseMenu, portalInfo.LevelID, portalInfo.Material);
                _portals.Add(portal);
            }

        }

        return _portals;
    }

    private void EnablePortals()
    {
        foreach(var portal in _portals)
        {
            portal.gameObject.SetActive(true);
        }

        _tutorial.Complited -= EnablePortals;
    }

    private void DisablePortals()
    {
        foreach (var portal in _portals)
        {
            portal.gameObject.SetActive(false);
        }
    }
}
