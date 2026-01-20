using System.Collections.Generic;
using UnityEngine;
using YG;

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

    public void SetQuests(Tutorial tutorial, LevelID level)
    {
        _tutorial = tutorial;

        if (level != LevelID.Tower)
        {
            DisablePortals();
            _tutorial.CompliteWithoutLust += EnablePortals;
        }
        else
        {
            EnableTowerPortal();
        }
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
                    portal.transform.localScale = portalInfo.Transform.localScale;
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
        foreach (Portal portal in _portals)
        {

            portal.gameObject.SetActive(true);
        }

        _tutorial.CompliteWithoutLust -= EnablePortals;
    }


    private void DisablePortals()
    {
        foreach (var portal in _portals)
        {
            portal.gameObject.SetActive(false);
        }
    }

    private void EnableTowerPortal()
    {
        foreach (var portal in _portals)
            portal.gameObject.SetActive(false);

        var firstPortal = _portals.Find(portal => portal.NextLevel == LevelID.Level1);
        firstPortal.gameObject.SetActive(true);

        if (YG2.saves.LevelsProgress == null || YG2.saves.LevelsProgress.Count == 0)
            return;

        for (int i = 1; i < _portals.Count; i++)
        {
            var prevLevelData = YG2.saves.LevelsProgress[i - 1];

            if (prevLevelData.IsComplite)
            {
                var portal = _portals.Find(portal => portal.NextLevel == YG2.saves.LevelsProgress[i - 1].Level +1);

                if (portal != null)
                    portal.gameObject.SetActive(true);
            }
            else
            {
                break;
            }
        }
    }
}
